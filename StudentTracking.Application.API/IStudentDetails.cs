using StudentTracking.Application.Models;
using System.Collections.Generic;

namespace StudentTracking.Application.API
{
    public interface IStudentDetails
    {
        IEnumerable<StudentModel> GetAll(int schoolId);
        StudentModel Get(int studentId);
        StudentModel Save(StudentModel student);
        IEnumerable<StudentModel> Find(int schoolId, string name, int classId, int sectionId);
        IEnumerable<StudentModel> Find(int schoolId, int classId, int sectionId);
    }
}
