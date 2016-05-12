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
        [Route("api/Security/{userId}/{password}")]
        public HttpResponseMessage Authenticate(string userId, string password)
        {
            return Request.CreateResponse(HttpStatusCode.OK, new UserContext
            {
                Id = userId,
                Name = "Test 1",
                Role = "Admin",
                SchoolId = "123",
                SecurityToken = Guid.NewGuid().ToString()
            });
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