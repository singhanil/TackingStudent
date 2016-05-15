using StudentTracking.Data;
using System.Collections.Generic;

namespace StudentTracking.Application.API
{
    public interface IStudentDetails
    {
        IEnumerable<StudentDetail> GetAll(int schoolId);
        StudentDetail Get(int schoolId, string studentId);
        StudentDetail Save(StudentDetail student);
    }
}
