
namespace StudentTracking.Application.Models
{
    public class SyllabusModel : ModelBase
    {
        public int ID { get; set; }
        public int SchoolId { get; set; }
        public int ClassId { get; set; }
        public int SectionId { get; set; }
        public int SubjectId { get; set; }
        public string Semester { get; set; }
        public string Detail { get; set; }
        public int TotalMarks { get; set; }

        public string ClassName { get; set; }
        public string SectionName { get; set; }
        public string Subject { get; set; }
    }
}
