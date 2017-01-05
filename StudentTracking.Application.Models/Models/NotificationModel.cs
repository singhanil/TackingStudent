
namespace StudentTracking.Application.Models
{
    public class NotificationModel : ModelBase
    {
        public int MessageId { get; set; }
        public int SchoolId { get; set; }
        public int ClassId { get; set; }
        public int SectionId { get; set; }
        public string StudentId { get; set; }
        public string MessageType { get; set; }
        public string Subject { get; set; }
        public string MessageText { get; set; }
        public string FilePath { get; set; }
        public System.DateTime CreatedDate { get; set; }
    }

    public class NotificationNew : ModelBase
    {
        public int MessageId { get; set; }
        public int SchoolId { get; set; }
        public int ClassId { get; set; }
        public int SectionId { get; set; }
        public string StudentId { get; set; }
        public string MessageType { get; set; }
        public string Subject { get; set; }
        public string MessageText { get; set; }
        public string FilePath { get; set; }
        public System.DateTime CreatedDate { get; set; }
    }
}
