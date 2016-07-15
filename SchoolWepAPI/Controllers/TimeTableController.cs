using StudentTracking.Application.API;
using StudentTracking.Application.Main;
using StudentTracking.Application.Models;
using StudentTracking.Application.Models.Requests;
using StudentTracking.Application.Models.Responses;
using StudentTracking.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SchoolWepAPI.Controllers
{
    public class TimeTableController : BaseApiController
    {
        private StudentTrackingContext _dbContext;

        public TimeTableController()
        {
            this._dbContext = new StudentTrackingContext();
        }

        [Route("api/TimeTable/{securityToken}/{schoolId}")]
        public HttpResponseMessage Get(string securityToken, int schoolId)
        {
            TTCommonResponse response = null;
            if (IsValid(securityToken))
            {
                ITimeTable ttService = new TimeTableService(this._dbContext);
                response = new TTCommonResponse { Status = "OK" };
                response.Subjects = ttService.GetSubjects(schoolId);
                response.Lectures = ttService.GetLectures(schoolId);

                CurrentLoggerProvider.Info(string.Format("Retrieved Common Data. Subjects: {0}, Lectures: {1}", response.Subjects.Count(), response.Lectures.Count()));
            }
            else
            {
                response = new TTCommonResponse { Status = "Error", ErrorCode = "ERR1001", ErrorMessage = "Invalid or expired token" };
                CurrentLoggerProvider.Info("Invalid Request");
            }

            return Request.CreateResponse(HttpStatusCode.OK, response);
        }

        [Route("api/TimeTable/{securityToken}/{schoolId}/{classId}/{sectionId}")]
        public HttpResponseMessage Get(string securityToken, int schoolId, int classId, int sectionId)
        {
            TimeTableResponse response = null;
            if (IsValid(securityToken))
            {
                ITimeTable ttService = new TimeTableService(this._dbContext);
                response = new TimeTableResponse { Status = "OK" };
                response.TimeTables = ttService.FindAll(schoolId, classId, sectionId);

                CurrentLoggerProvider.Info(string.Format("Retrieved Time Table. Count = {0}", response.TimeTables.Count()));
            }
            else
            {
                response = new TimeTableResponse { Status = "Error", ErrorCode = "ERR1001", ErrorMessage = "Invalid or expired token" };
                CurrentLoggerProvider.Info("Invalid Request");
            }

            return Request.CreateResponse(HttpStatusCode.OK, response);
        }

        [Route("api/TimeTable/Mobile/{securityToken}/{schoolId}/{classId}/{sectionId}")]
        public HttpResponseMessage GetTimeTable(string securityToken, int schoolId, int classId, int sectionId)
        {
            TimeTableVMResponse response = null;
            if (IsValid(securityToken))
            {
                ITimeTable ttService = new TimeTableService(this._dbContext);
                response = new TimeTableVMResponse { Status = "OK" };
                response.Monday = ttService.Get(schoolId, classId, sectionId, "Monday");
                response.Tuesday = ttService.Get(schoolId, classId, sectionId, "Tuesday");
                response.Wednessday = ttService.Get(schoolId, classId, sectionId, "Wednessday");
                response.Thursday = ttService.Get(schoolId, classId, sectionId, "Thursday");
                response.Friday = ttService.Get(schoolId, classId, sectionId, "Friday");

                CurrentLoggerProvider.Info("Retrieved time for mobile view");
            }
            else
            {
                response = new TimeTableVMResponse { Status = "Error", ErrorCode = "ERR1001", ErrorMessage = "Invalid or expired token" };
                CurrentLoggerProvider.Info("Invalid Request");
            }

            return Request.CreateResponse(HttpStatusCode.OK, response);
        }

        private bool IsValid(string securityToken)
        {
            ISecurity auth = new SecurityService(this._dbContext);
            return auth.ValidateToken(securityToken);
        }
        [HttpPost]
        public HttpResponseMessage Save(TimeTableRequest request)
        {
            var result = false;
            if(IsValid(request.SecurityToken))
            {
                var svc = new TimeTableService(this._dbContext);
                svc.Save(request.SchoolId, request.ClassId, request.SectionId, request.TimeTable);
                result = true;
            }
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }
        /*
        [HttpPost]
        public HttpResponseMessage SaveBulk(BulkTimeTableRequest request)
        {
            var result = false;
            if (IsValid(request.SecurityToken))
            {
                var svc = new TimeTableService(this._dbContext);
                result = svc.SaveBulk(request.TimeTables);
            }
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }*/
    }
}