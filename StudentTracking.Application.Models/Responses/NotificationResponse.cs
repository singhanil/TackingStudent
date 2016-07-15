using StudentTracking.Domain;
using System.Collections.Generic;

namespace StudentTracking.Application.Models.Responses
{
    public class NotificationResponse : ServiceResponse
    {
        public IEnumerable<NotificationModel> Notifications { get; set; }
    }
    public class NotificationSaveResponse : ServiceResponse
    {
    }
}
