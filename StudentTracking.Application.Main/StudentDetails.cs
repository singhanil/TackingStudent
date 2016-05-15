using StudentTracking.Application.API;
using StudentTracking.Data;

using System;
using System.Collections.Generic;
using System.Linq;

namespace StudentTracking.Application.Main
{
    public class StudentDetails : IStudentDetails
    {
        StudentTrackingContext _dbContext = null;

        public StudentDetails(StudentTrackingContext cntx)
        {
            this._dbContext = cntx;
        }

        public IEnumerable<StudentDetail> GetAll(int schoolId)
        {
            return this._dbContext.StudentDetails.Where(sd => sd.SchoolBranchId == schoolId).ToList();
        }

        public StudentDetail Get(int schoolId, string studentId)
        {
            return this._dbContext.StudentDetails
                .Where(sd => sd.SchoolBranchId == schoolId && sd.StudentId.Equals(studentId, StringComparison.InvariantCultureIgnoreCase))
                .FirstOrDefault();
        }
        public StudentDetail Save(StudentDetail student)
        {
            return student;
        }
    }
}
