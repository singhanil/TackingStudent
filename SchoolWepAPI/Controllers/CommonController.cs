using StudentTracking.Application.API;
using StudentTracking.Application.Main;
using StudentTracking.Application.Models;
using StudentTracking.Application.Models.Responses;
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
                response.Coutries = commonSvc.GetAllCountries();
                response.States = commonSvc.GetAllStates();

                CurrentLoggerProvider.Info(string.Format("Retrieved Master Data"));
            }
            else
            {
                response = new MasterDataResponse { Status = "Error", ErrorCode = "ERR1001", ErrorMessage = "Invalid or expired token" };
                CurrentLoggerProvider.Info("Invalid Request");
            }

            return Request.CreateResponse(HttpStatusCode.OK, response);
        }

        [Route("api/Common/GetStates/{securityToken}/{countryCode}")]
        public HttpResponseMessage GetStates(string securityToken, string countryCode)
        {
            StatesResponse response = null;
            if (IsValid(securityToken))
            {
                ICommon commonSvc = new CommonService(this._dbContext);
                response = new StatesResponse { Status = "OK" };
                response.States = commonSvc.FindStates(countryCode);

                if (null == response.States)
                {
                    response.Status = "Error";
                    response.ErrorMessage = string.Format("States not found for country: {0}", countryCode);
                    CurrentLoggerProvider.Info(response.ErrorMessage);
                }
                else
                    CurrentLoggerProvider.Info(string.Format("Retrieved States for Country: {0}", countryCode));
            }
            else
            {
                response = new StatesResponse { Status = "Error", ErrorCode = "ERR1001", ErrorMessage = "Invalid or expired token" };
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