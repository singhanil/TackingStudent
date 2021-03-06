﻿using StudentTracking.Application.API;
using StudentTracking.Application.Main;
using StudentTracking.Application.Models.Requests;
using StudentTracking.Application.Models.Responses;
using StudentTracking.Data;

using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SchoolWepAPI.Controllers
{
    public class StudentController : BaseApiController
    {
        StudentTrackingContext _dbContext = null;

        public StudentController()
        {
            this._dbContext = new StudentTrackingContext();
        }

        [Route("api/Student/{securityToken}/{schoolId}")]
        public HttpResponseMessage Get(string securityToken, int schoolId)
        {

            AllStudentResponse response = null;
            if (IsValid(securityToken))
            {
                IStudentDetails student = new StudentService(this._dbContext);
                response = new AllStudentResponse { Status = "OK" };
                response.Students = student.GetAll(schoolId);
                CurrentLoggerProvider.Info(string.Format("Retrieved Student Details. Count = {0}", response.Students.Count()));
            }
            else
            {
                response = new AllStudentResponse { Status = "Error", ErrorCode = "ERR1001", ErrorMessage = "Invalid or expired token" };
                CurrentLoggerProvider.Info("Invalid Request. Student Id: {0}");
            }

            return Request.CreateResponse(HttpStatusCode.OK, response);
        }

        [Route("api/Student/{securityToken}/{schoolId}/{name}/{classId}/{sectionId}")]
        public HttpResponseMessage Get(string securityToken, int schoolId, string name, int classId, int sectionId)
        {

            AllStudentResponse response = null;
            if (IsValid(securityToken))
            {
                IStudentDetails student = new StudentService(this._dbContext);
                response = new AllStudentResponse { Status = "OK" };
                response.Students = student.Find(schoolId, name, classId, sectionId);
                //CurrentLoggerProvider.Info(string.Format("Retrieved Student Details. Count = {0}", response.Students.Count()));
            }
            else
            {
                response = new AllStudentResponse { Status = "Error", ErrorCode = "ERR1001", ErrorMessage = "Invalid or expired token" };
                CurrentLoggerProvider.Info("Invalid Request. Student Id: {0}");
            }

            return Request.CreateResponse(HttpStatusCode.OK, response);
        }

        [Route("api/Student/{securityToken}/{schoolId}/{classId}/{sectionId}")]
        public HttpResponseMessage Get(string securityToken, int schoolId, int classId, int sectionId)
        {

            AllStudentResponse response = null;
            if (IsValid(securityToken))
            {
                IStudentDetails student = new StudentService(this._dbContext);
                response = new AllStudentResponse { Status = "OK" };
                response.Students = student.Find(schoolId, classId, sectionId);
                CurrentLoggerProvider.Info(string.Format("Retrieved Student Details. Count = {0}", response.Students.Count()));
            }
            else
            {
                response = new AllStudentResponse { Status = "Error", ErrorCode = "ERR1001", ErrorMessage = "Invalid or expired token" };
                CurrentLoggerProvider.Info("Invalid Request. Student Id: {0}");
            }

            return Request.CreateResponse(HttpStatusCode.OK, response);
        }

        private bool IsValid(string securityToken)
        {
            ISecurity auth = new SecurityService(this._dbContext);
            return auth.ValidateToken(securityToken);
        }

        [Route("api/Student/GetStudent/{securityToken}/{studentId}")]
        public HttpResponseMessage GetStudent(string securityToken, int studentId)
        {
            StudentResponse response = null;
            if (IsValid(securityToken))
            {
                IStudentDetails student = new StudentService(this._dbContext);
                response = new StudentResponse { Status = "OK" };
                response.Student = student.Get(studentId);
                CurrentLoggerProvider.Info(string.Format("Retrieved Student Details. Id = {0}", response.Student.Id));
            }
            else
            {
                response = new StudentResponse { Status = "Error", ErrorCode = "ERR1001", ErrorMessage = "Invalid or expired token" };
                CurrentLoggerProvider.Info(string.Format("Invalid Request. Student Id: {0}", studentId));
            }

            return Request.CreateResponse(HttpStatusCode.OK, response);
        }

        // POST api/values
        [HttpPost]
        public HttpResponseMessage Save(StudentSaveRequest req)
        {
            StudentResponse res = new StudentResponse { Status = "OK" };
            if (IsValid(req.SecurityToken))
            {
                var studentSvc = new StudentService(this._dbContext);
                res.Student = studentSvc.Save(req.Student);
            }
            else
            {
                res = new StudentResponse { Status = "Error", ErrorCode = "ERR1001", ErrorMessage = "Invalid or expired token" };
                CurrentLoggerProvider.Info(string.Format("Invalid Request. Student Id: {0}", req.Student.Id));
            }
            return Request.CreateResponse(HttpStatusCode.OK, res);
        }
    }
}
