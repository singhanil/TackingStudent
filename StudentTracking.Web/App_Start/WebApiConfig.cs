using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace StudentTracking.Web
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new {id = RouteParameter.Optional}
             );
            config.Routes.MapHttpRoute(
                name: "testApi",
                routeTemplate: "testApi/{controller}/{tagId}/{ipAddress}/{isIntime}",
                defaults: new { tagId = RouteParameter.Optional, ipAddress = RouteParameter.Optional,ipIntime = RouteParameter.Optional }
             );
           
        }
    }
}
