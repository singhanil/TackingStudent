using StudentTracking.Application.API;
using StudentTracking.Application.Main;
using StudentTracking.Data;
using StudentTracking.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SchoolWepAPI.Controllers
{
    public class SecurityController : BaseApiController
    {
        StudentTrackingContext __dbContext = null;

        public SecurityController()
        {
            this.__dbContext = new StudentTrackingContext();
        }
        [HttpGet]
        [Route("api/Security/{userId}/{password}")]
        public HttpResponseMessage Authenticate(string userId, string password)
        {
            AuthResponse response = new AuthResponse();

            ISecurity auth = new Security(this.__dbContext);
            var uc = auth.Authenticate(userId, password);
            if(null != uc)
            {
                response.Status="OK";
                response.UserContext = uc;
            }
            else
                response = new AuthResponse{Status="Failed", ErrorCode="AU1001", ErrorMessage="Invalid username or password."};
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
    }
}