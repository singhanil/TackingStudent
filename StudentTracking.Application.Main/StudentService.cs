using StudentTracking.Application.API;
using StudentTracking.Application.Models;
using StudentTracking.Data;
using StudentTracking.Application.Main.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;

namespace StudentTracking.Application.Main
{
    public class StudentService : IStudentDetails
    {
        StudentTrackingContext _dbContext = null;

        public StudentService(StudentTrackingContext cntx)
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
                .Where(sd => sd.ID == studentId)
                .FirstOrDefault();
            if (null != entity)
                return entity.MapAs<StudentDetail, StudentModel>();
            return null;
        }
        public StudentModel Save(StudentModel model)
        {
            var entity = this._dbContext.StudentDetails.Where(e => e.ID == model.Id).FirstOrDefault();

            entity = _populateValues(entity, model);

            if (model.Id > 0)
            {
                if (null != entity)
                    this._dbContext.Entry(entity).State = EntityState.Modified;
                else
                    this._dbContext.StudentDetails.Add(entity);
            }
            else
                this._dbContext.StudentDetails.Add(entity);

            this._dbContext.SaveChanges();

            //Create user account when student gets registered.
            //create parent's account as well.

            var securitySVC = new SecurityService(this._dbContext);
            var userModel = new UserModel
            {
                UserId = entity.StudentId,
                UserRole = "Student",
                Password = "Welcome1@",
                Name = entity.StudentName,
                SchoolId = entity.SchoolBranchId
            };
            securitySVC.Save(userModel);

            return entity.MapAs<StudentDetail, StudentModel>();
        }

        private StudentDetail _populateValues(StudentDetail entity, StudentModel model)
        {
            if (null == entity)
            {
                entity = new StudentDetail();

                entity.StudentId = model.StudentId;
                entity.EmailId = model.EmailId;
                entity.SchoolBranchId = model.SchoolBranchId;
            }
            entity.Address1 = model.Address1;
            entity.Address2 = model.Address2;
            entity.City = model.City;
            entity.Country = model.Country;
            entity.State = model.State;
            entity.ClassId = model.ClassId;
            entity.DateOfBirthh = model.DateOfBirthh;
            entity.Gender = model.Gender;
            entity.ParentMobileNo = model.ParentMobileNo;
            entity.ParentName = model.ParentName;
            entity.PrimaryTagId = model.PrimaryTagId;
            entity.SecondaryTagId = model.SecondaryTagId;
            entity.SectionId = model.SectionId;
            entity.StudentName = model.StudentName;
            entity.ZipCode = model.ZipCode;
                
            return entity;
        }
    }
}
