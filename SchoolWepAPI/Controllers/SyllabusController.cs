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
    public class SyllabusController : BaseApiController
    {
        private StudentTrackingContext _dbContext;

        public SyllabusController()
        {
            this._dbContext = new StudentTrackingContext();
        }

        [Route("api/Syllabus/{securityToken}/{schoolId}/{classId}/{sectionId}")]
        public HttpResponseMessage Get(string securityToken, int schoolId, int classId, int sectionId)
        {
            SyllabusResponse response = null;
            if (IsValid(securityToken))
            {
                ISyllabusService syllabusService = new SyllabusService(this._dbContext);
                response = new SyllabusResponse { Status = "OK" };
                response.Syllabus = syllabusService.Get(schoolId, classId, sectionId);

                CurrentLoggerProvider.Info(string.Format("Retrieved Syllabus. Count = {0}", response.Syllabus.Count()));
            }
            else
            {
                response = new SyllabusResponse { Status = "Error", ErrorCode = "ERR1001", ErrorMessage = "Invalid or expired token" };
                CurrentLoggerProvider.Info("Invalid Request");
            }

            return Request.CreateResponse(HttpStatusCode.OK, response);
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

        private bool IsValid(string securityToken)
        {
            ISecurity auth = new SecurityService(this._dbContext);
            return auth.ValidateToken(securityToken);
        }
    }
}