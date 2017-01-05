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
    public class NotificationController : BaseApiController
    {
        private StudentTrackingContext _dbContext;

        public NotificationController()
        {
            this._dbContext = new StudentTrackingContext();
        }

        [Route("api/Notification/Mobile/{securityToken}/{schoolId}/{userId}")]
        public HttpResponseMessage Get(string securityToken, int schoolId, string userId)
        {
            NotificationResponse response = null;
            if (IsValid(securityToken))
            {
                INotificationService notificationService = new NotificationService(this._dbContext);
                response = new NotificationResponse { Status = "OK" };
                response.Notifications = notificationService.Get(schoolId, userId);

                CurrentLoggerProvider.Info(string.Format("Retrieved Notifications. Count = {0}", response.Notifications.Count()));
            }
            else
            {
                response = new NotificationResponse { Status = "Error", ErrorCode = "ERR1001", ErrorMessage = "Invalid or expired token" };
                CurrentLoggerProvider.Info("Invalid Request");
            }

            return Request.CreateResponse(HttpStatusCode.OK, response);
        }

        [HttpPost]
        public HttpResponseMessage Save()
        {
           
            var studentSvc = new NotificationService(this._dbContext);
            NotificationSaveResponse res = new NotificationSaveResponse { Status = "OK" };
            var SecurityToken = System.Web.HttpContext.Current.Request.Form["SecurityToken"];
            if (IsValid(SecurityToken))
            {
                //var studentSvc = new NotificationService(this._dbContext);
                var result = studentSvc.PostFormData();
                res.ErrorMessage = result.ReasonPhrase;
                //studentSvc.Save(req.Notification);
            }
            else
            {
                res = new NotificationSaveResponse { Status = "Error", ErrorCode = "ERR1001", ErrorMessage = "Invalid or expired token" };
                CurrentLoggerProvider.Info(string.Format("Invalid Request. Student Id: {0}", 0));
            }
            return Request.CreateResponse(HttpStatusCode.OK, res);
        }
        //[HttpPost]
        //public HttpResponseMessage Save(NotificationRequest req)
        //{
        //    NotificationSaveResponse res = new NotificationSaveResponse { Status = "OK" };
        //    if (IsValid(req.SecurityToken))
        //    {
        //        var studentSvc = new NotificationService(this._dbContext);
        //        studentSvc.Save(req.Notification);
        //    }
        //    else
        //    {
        //        res = new NotificationSaveResponse { Status = "Error", ErrorCode = "ERR1001", ErrorMessage = "Invalid or expired token" };
        //        CurrentLoggerProvider.Info(string.Format("Invalid Request. Student Id: {0}", req.Notification.MessageId));
        //    }
        //    return Request.CreateResponse(HttpStatusCode.OK, res);
        //}
        private bool IsValid(string securityToken)
        {
            ISecurity auth = new SecurityService(this._dbContext);
            return auth.ValidateToken(securityToken);
        }
    }
}
