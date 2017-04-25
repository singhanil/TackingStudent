using StudentTracking.Application.API;
using StudentTracking.Application.Main;
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
    public class GalleryController : BaseApiController
    {
        private StudentTrackingContext _dbContext;

        public GalleryController()
        {
            this._dbContext = new StudentTrackingContext();
        }

        [HttpGet]
        [Route("api/Gallery/{securityToken}/{SchoolId}/{PageNumber}/{PageSize}")]
        public HttpResponseMessage Get(string securityToken, int schoolId, int pageNumber, int pageSize)
        {
            var response = new GalleryResponse { Status = "OK", SchoolId = schoolId };
            if (IsValid(securityToken))
            {
                var svc = new GalleryService(this._dbContext);
                int count = 0;
                response.Images = svc.Get(schoolId, pageNumber, pageSize, out count);
                response.ImageCount = count;
            }
            else
            {
                response = new GalleryResponse { Status = "Error", ErrorCode = "ERR001", ErrorMessage = "Invalide security token" };
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