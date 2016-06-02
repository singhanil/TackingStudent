using StudentTracking.Application.API;
using StudentTracking.Application.Main;
using StudentTracking.Application.Models.Requests;
using StudentTracking.Application.Models.Responses;
using StudentTracking.Data;

using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SchoolWepAPI.Controllers
{
    public class OrganizationController : BaseApiController
    {
        private StudentTrackingContext _dbContext;


        public OrganizationController()
        {
            this._dbContext = new StudentTrackingContext();
        }
        /// <summary>
        /// Get the list of school
        /// </summary>
        /// <param name="securityToken"></param>
        /// <returns></returns>
        [Route("api/Organization/{securityToken}")]
        public HttpResponseMessage Get(string securityToken)
        {
            OrganizationsResponse response = null;
            if (IsValid(securityToken))
            {
                IOrganization org = new OrganizationService(this._dbContext);

                response = new OrganizationsResponse { Status = "OK" };
                response.Organizations = org.GetAll();
                CurrentLoggerProvider.Info(string.Format("Retrieved Organizations. Count = {0}", response.Organizations.Count()));
            }
            else
            {
                response = new OrganizationsResponse { Status = "Error", ErrorCode = "ERR1001", ErrorMessage = "Invalid or expired token" };
            }

            return Request.CreateResponse(HttpStatusCode.OK, response);
        }

        [Route("api/Organization/{securityToken}/{Id}")]
        public HttpResponseMessage Get(string securityToken, int Id)
        {
            OrganizationResponse response = null;
            if (IsValid(securityToken))
            {
                IOrganization org = new OrganizationService(this._dbContext);

                response = new OrganizationResponse { Status = "OK" };
                try
                {
                    response.Organization = org.Get(Id);
                    if (null == response.Organization)
                    {
                        response.ErrorCode = "ERR1002";
                        response.ErrorMessage = string.Format("Invalid Organization Id: {0}", Id);
                        response.Status = "Error";
                        CurrentLoggerProvider.Error(string.Format("Invalid Request. Organization Id: {0}", Id));
                    }
                    else
                        CurrentLoggerProvider.Info(string.Format("Retrieved Organization. Id = {0}, Name={1}", response.Organization.ID, response.Organization.Name));
                }
                catch (Exception e)
                {
                    CurrentLoggerProvider.Error(string.Format("Error whiling retrieving Organization. Id = {0}", Id), e);
                    response.ErrorCode = "ERR1003";
                    response.ErrorMessage = string.Format("Unable to process request due to technical issue(s). Organization Id: {0}", Id);
                    response.Status = "Error";
                }
            }
            else
            {
                response = new OrganizationResponse { Status = "Error", ErrorCode = "ERR1001", ErrorMessage = "Invalid or expired token" };
            }
            return Request.CreateResponse(HttpStatusCode.OK, response);
        }

        private bool IsValid(string securityToken)
        {
            ISecurity auth = new SecurityService(this._dbContext);
            return auth.ValidateToken(securityToken);
        }

        [HttpPost]
        public HttpResponseMessage Save(OrganizationSaveRequest request)
        {
            OrganizationResponse response = new OrganizationResponse { Status = "OK" };
            if (IsValid(request.SecurityToken))
            {
                var orgSvc = new OrganizationService(this._dbContext);
                response.Organization = orgSvc.Save(request.Organization);
            }
            else
            {
                response = new OrganizationResponse { Status = "Error", ErrorCode = "ERR1001", ErrorMessage = "Invalid or expired token" };
            }


            return Request.CreateResponse(HttpStatusCode.OK, response);
        }
    }
}