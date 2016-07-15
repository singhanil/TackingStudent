
namespace StudentTracking.Application.Models
{
    public class NotificationModel : ModelBase
    {
        public string MessageId { get; set; }
        public int SchoolId { get; set; }
        public string To { get; set; }
        public string From { get; set; }
        public string Message { get; set; }
        public string RelatedMSGId { get; set; }
        public bool IsNew { get; set; }
    }
}
