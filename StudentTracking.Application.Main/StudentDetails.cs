using StudentTracking.Application.API;
using StudentTracking.Application.Models;
using StudentTracking.Data;
using StudentTracking.Application.Main.Extensions;
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

        public IEnumerable<StudentModel> GetAll(int schoolId)
        {
            var entities =  this._dbContext.StudentDetails.Where(sd => sd.SchoolBranchId == schoolId).ToList();

            return entities.MapAsCollection<StudentDetail, StudentModel>();
        }

        public StudentModel Get(int studentId)
        {
            var entity =  this._dbContext.StudentDetails
                .Where(sd => sd.Id == studentId)
                .FirstOrDefault();
            if (null != entity)
                return entity.MapAs<StudentDetail, StudentModel>();
            return null;
        }
        public StudentModel Save(StudentModel student)
        {
            return student;
        }
    }
}
