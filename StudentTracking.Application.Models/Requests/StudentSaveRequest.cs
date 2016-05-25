
namespace StudentTracking.Application.Models.Requests
{
    public class StudentSaveRequest : ModelBase
    {
        public string SecurityToken { get; set; }
        public StudentModel Student { get; set; }
    }
}
