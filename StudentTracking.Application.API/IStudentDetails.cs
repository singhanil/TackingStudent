using StudentTracking.Application.Models;
using System.Collections.Generic;

namespace StudentTracking.Application.API
{
    public interface IStudentDetails
    {
        IEnumerable<StudentModel> GetAll(int schoolId);
        StudentModel Get(int studentId);
        StudentModel Save(StudentModel student);
    }
}
