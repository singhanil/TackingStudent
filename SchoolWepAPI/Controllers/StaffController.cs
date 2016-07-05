using StudentTracking.Application.API;
using StudentTracking.Application.Main;
using StudentTracking.Application.Main.Extensions;
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
    public class StaffController : BaseApiController
    {
         private StudentTrackingContext _dbContext;


         public StaffController()
        {
            this._dbContext = new StudentTrackingContext();
        }

         [Route("api/Staff/Department/{securityToken}")]
         public HttpResponseMessage Get(string securityToken)
         {
             var response = new DepartmentResponse { Status = "OK" };
             if (IsValid(securityToken))
             {
                 var svc = new StaffService(this._dbContext);
                 response.Departments = svc.GetDepartment();
             }
             return Request.CreateResponse(HttpStatusCode.OK, response);
         }

        [Route("api/Staff/{securityToken}/{schoolId}")]
        public HttpResponseMessage Get(string securityToken, int schoolId)
        {
            var response = new StaffsResponse { Status = "OK" };
            if(IsValid(securityToken))
            {
                var entities = this._dbContext.StaffDetails.Where(sd => sd.SchoolId == schoolId).ToList();
                if (entities.Count > 0)
                    response.Staffs = entities.MapAsCollection<StaffDetail, StaffModel>();
                else
                    response.Staffs = new List<StaffModel>();
            }
            return Request.CreateResponse(HttpStatusCode.OK, response);
        }

        [Route("api/Staff/{securityToken}/{schoolId}/{departmentId}")]
        public HttpResponseMessage Get(string securityToken, int schoolId, int departmentId)
        {
            var response = new StaffsResponse { Status = "OK" };
            if (IsValid(securityToken))
            {
                var entities = this._dbContext.StaffDetails.Where(sd => sd.SchoolId == schoolId && sd.DepartmentId == departmentId).ToList();
                if (entities.Count > 0)
                    response.Staffs = entities.MapAsCollection<StaffDetail, StaffModel>();
                else
                    response.Staffs = new List<StaffModel>();
            }
            return Request.CreateResponse(HttpStatusCode.OK, response);
        }

        [Route("api/Staff/{securityToken}/{schoolId}/{name}/{departmentId}")]
        public HttpResponseMessage Get(string securityToken, int schoolId, string name, int departmentId)
        {
            var response = new StaffsResponse { Status = "OK" };
            if (IsValid(securityToken))
            {
                var entities = this._dbContext.StaffDetails
                .Where(s => s.SchoolId == schoolId && s.StaffName.Contains(name))
                .ToList();

                if(departmentId>0 && entities.Count>0)
                {
                    entities = entities.Where(e => e.DepartmentId == departmentId).ToList();
                }
                if (entities.Count > 0)
                    response.Staffs = entities.MapAsCollection<StaffDetail, StaffModel>();
                else
                    response.Staffs = new List<StaffModel>();
            }
            return Request.CreateResponse(HttpStatusCode.OK, response);
        }
        private bool IsValid(string securityToken)
        {
            ISecurity auth = new SecurityService(this._dbContext);
            return auth.ValidateToken(securityToken);
        }

        [HttpPost]
        public HttpResponseMessage Save(StaffRequest req)
        {
            StaffResponse res = new StaffResponse { Status = "OK" };
            if (IsValid(req.SecurityToken))
            {
                var studentSvc = new StaffService(this._dbContext);
                res.Staff = studentSvc.Save(req.Staff);
            }
            else
            {
                res = new StaffResponse { Status = "Error", ErrorCode = "ERR1001", ErrorMessage = "Invalid or expired token" };
                CurrentLoggerProvider.Info(string.Format("Invalid Request. Student Id: {0}", req.Staff.StaffId));
            }
            return Request.CreateResponse(HttpStatusCode.OK, res);
        }
    }
}