using StudentTracking.Application.API;
using StudentTracking.Application.Main;
using StudentTracking.Application.Models;
using StudentTracking.Application.Models.Responses;
using StudentTracking.Data;
using StudentTracking.Domain;
using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Web;
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
                response.Subjects = commonSvc.GetAllSubjects();
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

        [Route("api/Common/UploadFile")]
        [HttpPost]
        public HttpResponseMessage UploadFile()
        {
            CommonService commonSvc = new CommonService(this._dbContext);
            DocumentResponse res = new DocumentResponse { Status = "OK" };
            var SecurityToken = System.Web.HttpContext.Current.Request.Form["SecurityToken"];
            if (IsValid(SecurityToken))
            {
                //var studentSvc = new NotificationService(this._dbContext);
                var result = commonSvc.UploadDocument();
                res.ErrorMessage = result.ReasonPhrase;
                //studentSvc.Save(req.Notification);
            }
            else
            {
                res = new DocumentResponse { Status = "Error", ErrorCode = "ERR1001", ErrorMessage = "Invalid or expired token" };
                CurrentLoggerProvider.Info(string.Format("Invalid Request. Student Id: {0}", 0));
            }
            return Request.CreateResponse(HttpStatusCode.OK, res);
            //FileUploadResponse response = null;
            //var securityToken = System.Web.HttpContext.Current.Request.Form["SecurityToken"];
            //if (IsValid(securityToken))
            //{
            //    ICommon commonSvc = new CommonService(this._dbContext);
            //    response = new FileUploadResponse { Status = "OK" };

            //        var result = SaveFile();

            //    response.ErrorMessage = result.ReasonPhrase;

            //}
            //else
            //{
            //    response = new FileUploadResponse { Status = "Error", ErrorCode = "ERR1001", ErrorMessage = "Invalid or expired token" };
            //    CurrentLoggerProvider.Info("Invalid Request");
            //}

            //return Request.CreateResponse(HttpStatusCode.OK, response);
        }
        private bool IsValid(string securityToken)
        {
            ISecurity auth = new SecurityService(this._dbContext);
            return auth.ValidateToken(securityToken);
        }

        private HttpResponseMessage SaveFile()
        {
            var UploadPath = "";
            string path = "";
            foreach (string file in HttpContext.Current.Request.Files)
            {
                var FileDataContent = System.Web.HttpContext.Current.Request.Files[file];
                var myObject = System.Web.HttpContext.Current.Request;
                var SchoolId = Convert.ToInt32(myObject.Form["SchoolId"]);
                var guidKey = Guid.NewGuid();
                if (FileDataContent != null && FileDataContent.ContentLength > 0)
                {
                    try
                    {
                        
                        var stream = FileDataContent.InputStream;
                        var fileName = FileDataContent.FileName;
                        var fileExtension = Path.GetExtension(fileName);
                        //string driveLetter = Path.GetPathRoot(Environment.CurrentDirectory);
                        UploadPath = AppDomain.CurrentDomain.BaseDirectory + "FileStore\\" + SchoolId + "\\";
                        Directory.CreateDirectory(UploadPath);
                        path = Path.Combine(UploadPath, fileName);

                        if (System.IO.File.Exists(path))
                            System.IO.File.Delete(path);
                        using (var fileStream = System.IO.File.Create(path))
                        {
                            stream.CopyTo(fileStream);
                            fileStream.Dispose();
                        }
                    }
                    catch (IOException ex)
                    {
                        //return nobj;
                        return new HttpResponseMessage()
                        {
                            StatusCode = System.Net.HttpStatusCode.InternalServerError,
                            Content = new StringContent("File upload failed"),
                            ReasonPhrase = path
                        };
                    }
                }
            }
            //return nobj;
            return new HttpResponseMessage()
            {
                StatusCode = System.Net.HttpStatusCode.OK,
                Content = new StringContent("File successfully uploaded"),
                ReasonPhrase = UploadPath
            };

        }
    }
}