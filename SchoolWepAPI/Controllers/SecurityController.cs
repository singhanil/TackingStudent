using StudentTracking.Application.API;
using StudentTracking.Application.Main;
using StudentTracking.Application.Models.Requests;
using StudentTracking.Application.Models.Responses;
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

            ISecurity auth = new SecurityService(this.__dbContext);
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

        [Route("api/Users/{securityToken}/{schoolId}")]
        public HttpResponseMessage GetAll(string securityToken, int schoolId)
        {
            UsersResponse response = new UsersResponse();
            if (isValid(securityToken))
            {
                ISecurity securitySvc = new SecurityService(this.__dbContext);
                response = new UsersResponse { Status = "OK" };
                response.Users = securitySvc.UsersList(schoolId);
            }
            else
            {
                response = new UsersResponse { Status = "Error", ErrorCode = "ERR1001", ErrorMessage = "Invalid or expired token" };
                CurrentLoggerProvider.Info(string.Format("Invalid Request. Security Token: {0}", securityToken));
            }

            return Request.CreateResponse(HttpStatusCode.OK, response);
        }

        [Route("api/Users/{securityToken}/{userId}")]
        public HttpResponseMessage Get(string securityToken, string userId)
        {
            UserResponse response = new UserResponse { Status = "OK" };
            if (isValid(securityToken))
            {
                ISecurity securitySvc = new SecurityService(this.__dbContext);
                response.User = securitySvc.Get(userId);
            }
            else
            {
                response = new UserResponse { Status = "Error", ErrorCode = "ERR1001", ErrorMessage = "Invalid or expired token" };
                CurrentLoggerProvider.Info(string.Format("Invalid Request. Security Token: {0}", securityToken));
            }

            return Request.CreateResponse(HttpStatusCode.OK, response);
        }

        [HttpPost]
        public HttpResponseMessage Save(UserRequest req)
        {
            UserResponse response = new UserResponse { Status = "OK" };
            if (isValid(req.SecurityToken))
            {
                ISecurity securitySvc = new SecurityService(this.__dbContext);
                response.User = securitySvc.Save(req.User);
            }
            else
            {
                response = new UserResponse { Status = "Error", ErrorCode = "ERR1001", ErrorMessage = "Invalid or expired token" };
                CurrentLoggerProvider.Info(string.Format("Invalid Request. Security Token: {0}", req.SecurityToken));
            }

            return Request.CreateResponse(HttpStatusCode.OK, response);

        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }

        private bool isValid(string securityToken)
        {
            ISecurity auth = new SecurityService(this.__dbContext);
            return auth.ValidateToken(securityToken);
        }
    }
}