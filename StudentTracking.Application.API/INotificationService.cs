using StudentTracking.Application.Models;
using System.Collections.Generic;

namespace StudentTracking.Application.API
{
    public interface INotificationService
    {
        IEnumerable<NotificationModel> Get(int schoolId, string userId);
        void Save(NotificationModel model);
    }
}
