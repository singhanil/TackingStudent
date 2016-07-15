
namespace StudentTracking.Application.Models
{
    public class SyllabusModel : ModelBase
    {
        public int ID { get; set; }
        public int SchoolId { get; set; }
        public int ClassId { get; set; }
        public int SubjectId { get; set; }
        public string FilePath { get; set; }

        public string ClassName { get; set; }
        public string SubjectName { get; set; }
    }
}
