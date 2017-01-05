using StudentTracking.Application.Models;
using StudentTracking.Data;
using System.Collections.Generic;

namespace StudentTracking.Application.API
{
    public interface INotificationService
    {
        IEnumerable<Models.NotificationNew> Get(int schoolId, string userId);
        void Save(NotificationModel model);
    }
}
