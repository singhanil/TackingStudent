using StudentTracking.Application.API;
using StudentTracking.Application.Main.Extensions;
using StudentTracking.Application.Models;
using StudentTracking.Data;

using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

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

        public IEnumerable<StudentModel> Find(int schoolId, int classId, int sectionId)
        {
            var entities = this._dbContext.StudentDetails
                .Where(s => s.SchoolBranchId == schoolId)
                .ToList();

            if (null != entities && entities.Count > 0)
            {
                if (classId > 0)
                    entities = entities.Where(s => s.ClassId == classId).ToList();
            }
            if (null != entities && entities.Count > 0)
            {
                if (sectionId > 0 && entities.Count > 0)
                    entities = entities.Where(s => s.SectionId == sectionId).ToList();
            }

            if (null != entities && entities.Count > 0)
                return entities.MapAsCollection<StudentDetail, StudentModel>();

            return new List<StudentModel>();
        }

        public IEnumerable<StudentModel> Find(int schoolId, string name, int classId, int sectionId)
        {
            var entities = this._dbContext.StudentDetails
                .Where(s => s.SchoolBranchId == schoolId && s.StudentName.Contains(name))
                .ToList();

            if (null != entities && entities.Count > 0)
            {
                if (classId > 0)
                    entities = entities.Where(s => s.ClassId == classId).ToList();
            }
            if (null != entities && entities.Count > 0)
            {
                if (sectionId > 0 && entities.Count > 0)
                    entities = entities.Where(s => s.SectionId == sectionId).ToList();
            }

            if (null != entities && entities.Count > 0)
                return entities.MapAsCollection<StudentDetail, StudentModel>();

            return new List<StudentModel>();
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
