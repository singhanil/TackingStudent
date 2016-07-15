using StudentTracking.Application.API;
using StudentTracking.Application.Main;
using StudentTracking.Application.Responses;
using StudentTracking.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SchoolWepAPI.Controllers
{
    public class ResultController : BaseApiController
    {
        private StudentTrackingContext _dbContext;

        public ResultController()
        {
            this._dbContext = new StudentTrackingContext();
        }

        [Route("api/Result/Mobile/{securityToken}/{schoolId}/{studentId}")]
        public HttpResponseMessage Get(string securityToken, int schoolId, string studentId)
        {
            var response = new ResultVMResponse { Status = "OK" };
            if (IsValid(securityToken))
            {
                var svc = new ResultService(this._dbContext);
                response.Results = svc.Get(schoolId, studentId);
            }
            else
            {
                response = new ResultVMResponse { Status = "Error", ErrorCode = "ERR001", ErrorMessage = "Invalide security token" };
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
