using StudentTracking.Domain;
using System.Collections.Generic;

namespace StudentTracking.Application.Models.Responses
{
    public class NotificationResponse : ServiceResponse
    {
        public IEnumerable<NotificationNew> Notifications { get; set; }
    }
    public class NotificationSaveResponse : ServiceResponse
    {
        public NotificationModel Notifications { get; set; }
    }
}
