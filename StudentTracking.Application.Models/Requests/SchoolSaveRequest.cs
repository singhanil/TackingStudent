
namespace StudentTracking.Application.Models.Requests
{
    public class SchoolSaveRequest : ModelBase
    {
        public string SecurityToken { get; set; }
        public SchoolModel School { get; set; }
    }
}
