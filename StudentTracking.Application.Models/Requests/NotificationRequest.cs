using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentTracking.Application.Models.Requests
{
    public class NotificationRequest: ServiceRequest
    {
        public NotificationModel Notification { get; set; }
    }
}
