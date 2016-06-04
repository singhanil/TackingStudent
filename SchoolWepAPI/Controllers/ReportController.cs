using StudentTracking.Application.API;
using StudentTracking.Application.Main;
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
    public class ReportController : ApiController
    {
        private StudentTrackingContext _dbContext;

        public ReportController()
        {
            this._dbContext = new StudentTrackingContext();
        }

        [Route("api/Report/{securityToken}/{schoolId}/{classId}/{sectionId}")]
        public HttpResponseMessage Get(string securityToken, int schoolId, int classId, int sectionId)
        {
            var response = new DailyReportResponse{Status="OK"};
            if(IsValid(securityToken))
            {
                var svc = new AttendenceReportService(this._dbContext);
                response.Reports = svc.GetDailyReport();
            }
            else
            {
                response = new DailyReportResponse { Status = "Error", ErrorCode = "ERR001", ErrorMessage = "Invalide security token" };
            }

            return Request.CreateResponse(HttpStatusCode.OK, response);
        }

        [Route("api/Report/{securityToken}/{schoolId}/{classId}/{sectionId}/{type}")]
        public HttpResponseMessage Get(string securityToken, int schoolId, int classId, int sectionId, string type)
        {
            var response = new MonthlyReportResponse { Status = "OK" };
            if (IsValid(securityToken))
            {
                var svc = new AttendenceReportService(this._dbContext);
                if (type.Equals("Month", StringComparison.InvariantCultureIgnoreCase))
                    response.Reports = svc.GetMonthlyReport();
                else
                    response.Reports = svc.GetYearlyReport();
            }
            else
            {
                response = new MonthlyReportResponse { Status = "Error", ErrorCode = "ERR001", ErrorMessage = "Invalide security token" };
            }

            return Request.CreateResponse(HttpStatusCode.OK, response);
        }

        private bool IsValid(string securityToken)
        {
            ISecurity auth = new SecurityService(this._dbContext);
            return auth.ValidateToken(securityToken);
        }
    }
}