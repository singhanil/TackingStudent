
namespace StudentTracking.Application.Models
{
    public class TimeTableModel : ModelBase
    {
        public int ID { get; set; }
        public int ClassId { get; set; }
        public int SectionId { get; set; }
        public int LectureId { get; set; }
        public int SubjectId { get; set; }
        public string Days { get; set; }

        public string ClassName { get; set; }
        public string SectionName { get; set; }
        public string LectureDetail { get; set; }
        public string Subject { get; set; }
    }
}
