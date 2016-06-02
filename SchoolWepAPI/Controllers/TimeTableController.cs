using StudentTracking.Application.API;
using StudentTracking.Application.Main;
using StudentTracking.Application.Models;
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

        [Route("api/TimeTable/{securityToken}/{classId}/{sectionId}")]
        public HttpResponseMessage Get(string securityToken, int classId, int sectionId)
        {
            TimeTableResponse response = null;
            if (IsValid(securityToken))
            {
                ITimeTable ttService = new TimeTableService(this._dbContext);
                response = new TimeTableResponse { Status = "OK" };
                response.TimeTables = ttService.FindAll(classId, sectionId);

                CurrentLoggerProvider.Info(string.Format("Retrieved Time Table. Count = {0}", response.TimeTables.Count()));
            }
            else
            {
                response = new TimeTableResponse { Status = "Error", ErrorCode = "ERR1001", ErrorMessage = "Invalid or expired token" };
                CurrentLoggerProvider.Info("Invalid Request");
            }

            return Request.CreateResponse(HttpStatusCode.OK, response);
        }

        private bool IsValid(string securityToken)
        {
            ISecurity auth = new SecurityService(this._dbContext);
            return auth.ValidateToken(securityToken);
        }
        // GET api/<controller>/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<controller>
        public void Post([FromBody]string value)
        {
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}