using StudentTracking.Application.API;
using StudentTracking.Application.Main;
using StudentTracking.Data;
using StudentTracking.Domain;

using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SchoolWepAPI.Controllers
{
    public class CommonController : BaseApiController
    {
        StudentTrackingContext _dbContext = null;

        public CommonController()
        {
            this._dbContext = new StudentTrackingContext();
        }

        [Route("api/Common/{securityToken}")]
        public HttpResponseMessage GetAll(string securityToken)
        {
            MasterDataResponse response = null;
            if (IsValid(securityToken))
            {
                ICommon commonSvc = new CommonService(this._dbContext);
                response = new MasterDataResponse { Status = "OK" };
                response.Classes = commonSvc.GetAllClasses();
                response.Sections = commonSvc.GetAllSections();
                response.TagDetails = commonSvc.GetAllTags();

                CurrentLoggerProvider.Info(string.Format("Retrieved Master Data"));
            }
            else
            {
                response = new MasterDataResponse { Status = "Error", ErrorCode = "ERR1001", ErrorMessage = "Invalid or expired token" };
                CurrentLoggerProvider.Info("Invalid Request");
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