using StudentTracking.Application.API;
using StudentTracking.Application.Models;
using StudentTracking.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentTracking.Application.Main
{
    public class NotificationService : INotificationService
    {
        private StudentTrackingContext _dbContext = null;
        public NotificationService(StudentTrackingContext cntx)
        {
            this._dbContext = cntx;
        }
        public IEnumerable<NotificationModel> Get(int schoolId, string userId)
        {
            var list = new List<NotificationModel>();
            list.Add(new NotificationModel { MessageId = "LSDFJLJ3438938DD", SchoolId = 10, From = "Adminstrator", To = "Akash", IsNew = true, Message = "IT assessment worksheet will be held on 28th July 2016, Tuesday." });
            list.Add(new NotificationModel { MessageId = "KDSKFK39439DKFJDD", SchoolId = 10, From = "Adminstrator", To = "Akash", IsNew = true, Message = "Kindly note that the functional telephone line of the school reception is 020-XX343XX33. You are requested to call on the given number only for any correspondence." });
            list.Add(new NotificationModel { MessageId = "KSDKFJ499944KKDK", SchoolId = 10, From = "Adminstrator", To = "Akash", IsNew = false, Message = "Kindly be informed that the school will remain closed on Wednesday, July 6, 2016 on account of Eid Ul Fitr (Ramzan Eid)" });
            return list;
        }
        public void Save(NotificationModel model)
        {

        }
    }
}
