using StudentTracking.Application.API;
using StudentTracking.Application.Main;
using StudentTracking.Data;
using StudentTracking.Application.Models.Responses;
using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using StudentTracking.Application.Models;
using StudentTracking.Application.Models.Requests;

namespace SchoolWepAPI.Controllers
{
    public class SchoolController : BaseApiController
    {
        private StudentTrackingContext _dbContext;


        public SchoolController()
        {
            this._dbContext = new StudentTrackingContext();
        }
        /// <summary>
        /// Get the list of school
        /// </summary>
        /// <param name="securityToken"></param>
        /// <returns></returns>
        [Route("api/School/{securityToken}")]
        public HttpResponseMessage Get(string securityToken)
        {
            AllSchoolsResponse response = null;
            if (IsValid(securityToken))
            {
                ISchool school = new SchoolService(this._dbContext);
                response = new AllSchoolsResponse { Status = "OK" };
                response.Schools = school.GetAll();
                CurrentLoggerProvider.Info(string.Format("Retrieved Schools. Count = {0}", response.Schools.Count()));
            }
            else
            {
                response = new AllSchoolsResponse { Status = "Error", ErrorCode = "ERR1001", ErrorMessage = "Invalid or expired token" };
                CurrentLoggerProvider.Info("Invalid Request. School Id: {0}");
            }

            return Request.CreateResponse(HttpStatusCode.OK, response);
        }

        [Route("api/School/{securityToken}/{Id}")]
        public HttpResponseMessage Get(string securityToken, int Id)
        {
            SchoolResponse response = null;
            if (IsValid(securityToken))
            {
                ISchool schools = new StudentTracking.Application.Main.SchoolService(this._dbContext);
                response = new SchoolResponse { Status = "OK" };
                try
                {
                    response.School = schools.Get(Id);
                    if(null == response.School)
                    {
                        response.ErrorCode = "ERR1002";
                        response.ErrorMessage = string.Format("Invalid School Id: {0}", Id);
                        response.Status = "Error";
                        CurrentLoggerProvider.Error(string.Format("Invalid Request. School Id: {0}", Id));
                    }
                    else
                        CurrentLoggerProvider.Info(string.Format("Retrieved School. Id = {0}, Name={1}", response.School.Id, response.School.Name));
                }
                catch(Exception e)
                {
                    CurrentLoggerProvider.Error(string.Format("Error whiling retrieving School. Id = {0}", Id), e);
                    response.ErrorCode = "ERR1003";
                    response.ErrorMessage = string.Format("Unable to process request due to technical issue(s). School Id: {0}", Id);
                    response.Status = "Error";
                }
            }
            else
            {
                response = new SchoolResponse { Status = "Error", ErrorCode = "ERR1001", ErrorMessage = "Invalid or expired token" };
                CurrentLoggerProvider.Info(string.Format("Invalid Request. School Id: {0}", Id));
            }
            return Request.CreateResponse(HttpStatusCode.OK, response);
        }

        private bool IsValid(string securityToken)
        {
            ISecurity auth = new SecurityService(this._dbContext);
            return auth.ValidateToken(securityToken);
        }

        // POST api/<controller>
        [HttpPost]
        public HttpResponseMessage Save(SchoolSaveRequest request)
        {
            SchoolResponse response = new SchoolResponse { Status = "OK" };
            if(IsValid(request.SecurityToken))
            {
                var schoolSvc = new SchoolService(this._dbContext);
                response.School = schoolSvc.Save(request.School);
            }
            else
            {
                response = new SchoolResponse { Status = "Error", ErrorCode = "ERR1001", ErrorMessage = "Invalid or expired token" };
                CurrentLoggerProvider.Info(string.Format("Invalid Request. School Id: {0}", request.School.Id));
            }


            return Request.CreateResponse(HttpStatusCode.OK, response);
        }

        // DELETE api/<controller>/5
        public void Delete(string securityToken, int schoolId)
        {

        }

        [Route("api/School/holidaylist/{securityToken}/{schoolId}")]
        public HttpResponseMessage getHolidayList(string securityToken, int schoolId)
        {
            AllHolidayResponse response = null;
            if (IsValid(securityToken))
            {
                ISchool school = new SchoolService(this._dbContext);
                response = new AllHolidayResponse { Status = "OK" };
                response.HolidayList = school.GetHolidayList(schoolId);
                CurrentLoggerProvider.Info(string.Format("Retrieved Holidays. Count = {0}", response.HolidayList!= null ? response.HolidayList.Count() : 0 ));
            }
            else
            {
                response = new AllHolidayResponse { Status = "Error", ErrorCode = "ERR1001", ErrorMessage = "Invalid or expired token" };
                CurrentLoggerProvider.Info(string.Format("Invalid Request. School Id: {0}", schoolId));
            }

            return Request.CreateResponse(HttpStatusCode.OK, response);
        }

        [Route("api/School/calendar/{securityToken}/{schoolId}")]
        public HttpResponseMessage getcalendar(string securityToken, int schoolId)
        {
            CalendarResponse response = null;
            if (IsValid(securityToken))
            {
                ISchool school = new SchoolService(this._dbContext);
                response = new CalendarResponse { Status = "OK" };
                response.eventlist = school.GetCalendarEvents(schoolId);
                CurrentLoggerProvider.Info(string.Format("Retrieved eventlist. Count = {0}", response.eventlist != null ? response.eventlist.Count() : 0));
            }
            else
            {
                response = new CalendarResponse { Status = "Error", ErrorCode = "ERR1001", ErrorMessage = "Invalid or expired token" };
                CurrentLoggerProvider.Info(string.Format("Invalid Request. School Id: {0}", schoolId));
            }

            return Request.CreateResponse(HttpStatusCode.OK, response);
        }

        [Route("api/School/importantlinks/{securityToken}/{schoolId}")]
        public HttpResponseMessage getImportantLinks(string securityToken, int schoolId)
        {
            ImportantLinkResponse response = null;
            if (IsValid(securityToken))
            {
                ISchool school = new SchoolService(this._dbContext);
                response = new ImportantLinkResponse { Status = "OK" };
                response.ImportantLinks = school.getImportantLinks(schoolId);
                CurrentLoggerProvider.Info(string.Format("Retrieved ipmortant links. Count = {0}", response.ImportantLinks != null ? response.ImportantLinks.Count() : 0));
            }
            else
            {
                response = new ImportantLinkResponse { Status = "Error", ErrorCode = "ERR1001", ErrorMessage = "Invalid or expired token" };
                CurrentLoggerProvider.Info(string.Format("Invalid Request. School Id: {0}", schoolId));
            }

            return Request.CreateResponse(HttpStatusCode.OK, response);
        }
        [Route("api/School/alerts/{securityToken}/{schoolId}")]
        public HttpResponseMessage getAlerts(string securityToken, int schoolId)
        {
            AlertResponse response = null;
            if (IsValid(securityToken))
            {
                ISchool school = new SchoolService(this._dbContext);
                response = new AlertResponse { Status = "OK" };
                response.Alerts = school.GetAlerts(schoolId);
                CurrentLoggerProvider.Info(string.Format("Retrieved ipmortant links. Count = {0}", response.Alerts != null ? response.Alerts.Count() : 0));
            }
            else
            {
                response = new AlertResponse { Status = "Error", ErrorCode = "ERR1001", ErrorMessage = "Invalid or expired token" };
                CurrentLoggerProvider.Info(string.Format("Invalid Request. School Id: {0}", schoolId));
            }

            return Request.CreateResponse(HttpStatusCode.OK, response);
        }

        [Route("api/School/getBlogUrl/{securityToken}/{schoolId}")]
        public HttpResponseMessage getBlogUrl(string securityToken, int schoolId)
        {
            BlogResponse response = null;
            if (IsValid(securityToken))
            {
                ISchool school = new SchoolService(this._dbContext);
                response = new BlogResponse { Status = "OK" };
                response.BlogUrl = school.GetBlogUrl(schoolId);
                CurrentLoggerProvider.Info(string.Format("Retrieved blog url = {0}", response.BlogUrl));
            }
            else
            {
                response = new BlogResponse { Status = "Error", ErrorCode = "ERR1001", ErrorMessage = "Invalid or expired token" };
                CurrentLoggerProvider.Info(string.Format("Invalid Request. School Id: {0}", schoolId));
            }

            return Request.CreateResponse(HttpStatusCode.OK, response);
        }

        [Route("api/School/Events/{securityToken}/{schoolId}")]
        public HttpResponseMessage getEvents(string securityToken, int schoolId)
        {
            EventResponse response = null;
            if (IsValid(securityToken))
            {
                ISchool school = new SchoolService(this._dbContext);
                response = new EventResponse { Status = "OK" };
                response.Events = school.GetEvents(schoolId);
                CurrentLoggerProvider.Info(string.Format("Retrieved Events: {0}", response.Events != null ? response.Events.Count() : 0));
            }
            else
            {
                response = new EventResponse { Status = "Error", ErrorCode = "ERR1001", ErrorMessage = "Invalid or expired token" };
                CurrentLoggerProvider.Info(string.Format("Invalid Request. School Id: {0}", schoolId));
            }

            return Request.CreateResponse(HttpStatusCode.OK, response);
        }
    }
}