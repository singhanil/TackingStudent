using StudentTracking.Domain;
using System.Collections.Generic;

namespace StudentTracking.Application.Models.Responses
{
    public class StudentResponse : ServiceResponse
    {
        public StudentModel Student { get; set; }
    }

    public class AllStudentResponse : ServiceResponse
    {
        public IEnumerable<StudentModel> Students { get; set; }
    }
}
