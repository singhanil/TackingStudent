using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using log4net;
using log4net.Core;
using System.Reflection;
namespace SchoolWepAPI.Controllers
{
    public class BaseApiController : ApiController
    {
        protected ILog CurrentLoggerProvider { get; private set; }

        public BaseApiController()
        {
            CurrentLoggerProvider = log4net.LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        }
    }
}