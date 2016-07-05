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

            if (entities.Count > 0)
                return entities.MapAsCollection<StudentDetail, StudentModel>();

            return new List<StudentModel>();
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
            var isNew = true;
            var entity = this._dbContext.StudentDetails.Where(e => e.ID == model.Id).FirstOrDefault();
            int primaryTagId=0;
            int? secondaryTagId=0;
            if (null != entity)
            {
                isNew = false;
                primaryTagId = entity.PrimaryTagId;
                secondaryTagId = entity.SecondaryTagId;
            }
            entity = _populateValues(entity, model);

            if (isNew)
            {
                this._dbContext.StudentDetails.Add(entity);
                //Set isAvailable flag to 'N'
                var primaryTagDetail = this._dbContext.TagDetails.Where(t => t.ID == entity.PrimaryTagId).FirstOrDefault();
                if (null != primaryTagDetail)
                {
                    primaryTagDetail.IsAvailable = "N";
                    this._dbContext.Entry<TagDetail>(primaryTagDetail).State = EntityState.Modified;
                }
                var secondaryTagDetail = this._dbContext.TagDetails.Where(t => t.ID == entity.SecondaryTagId).FirstOrDefault();
                if (null != secondaryTagDetail)
                {
                    secondaryTagDetail.IsAvailable = "N";
                    this._dbContext.Entry<TagDetail>(secondaryTagDetail).State = EntityState.Modified;
                }
            }
            else
            {
                this._dbContext.Entry<StudentDetail>(entity).State = EntityState.Modified;
                if(primaryTagId != entity.PrimaryTagId)
                {
                    var oldPrimaryTag = this._dbContext.TagDetails.Where(t => t.ID == primaryTagId).FirstOrDefault();
                    if(null != oldPrimaryTag)
                    {
                        oldPrimaryTag.IsAvailable = "Y";
                        this._dbContext.Entry<TagDetail>(oldPrimaryTag).State = EntityState.Modified;
                    }
                    var newPrimaryTag = this._dbContext.TagDetails.Where(t => t.ID == entity.PrimaryTagId).FirstOrDefault();
                    if (null != newPrimaryTag)
                    {
                        newPrimaryTag.IsAvailable = "N";
                        this._dbContext.Entry<TagDetail>(newPrimaryTag).State = EntityState.Modified;
                    }
                }
                if (secondaryTagId != entity.SecondaryTagId)
                {
                    var oldSecondaryTag = this._dbContext.TagDetails.Where(t => t.ID == primaryTagId).FirstOrDefault();
                    if (null != oldSecondaryTag)
                    {
                        oldSecondaryTag.IsAvailable = "Y";
                        this._dbContext.Entry<TagDetail>(oldSecondaryTag).State = EntityState.Modified;
                    }
                    var newSecondaryTag = this._dbContext.TagDetails.Where(t => t.ID == entity.PrimaryTagId).FirstOrDefault();
                    if (null != newSecondaryTag)
                    {
                        newSecondaryTag.IsAvailable = "N";
                        this._dbContext.Entry<TagDetail>(newSecondaryTag).State = EntityState.Modified;
                    }
                }
            }
            this._dbContext.SaveChanges();

            //Create user account when student gets registered.
            //create parent's account as well.
            if (isNew)
            {
                var securitySVC = new SecurityService(this._dbContext);
                var userModel = new UserModel
                {
                    UserId = entity.StudentId,
                    UserRole = "Student",
                    Password = "Welcome1@",
                    Name = entity.StudentName,
                    SchoolId = entity.SchoolBranchId.Value,
                    ClassId = entity.ClassId,
                    SectionId = entity.SectionId,
                    EmailId= entity.EmailId,
                    ContactNumber = entity.ParentMobileNo
                };
                securitySVC.Save(userModel);
                //Create account of parent as well
                userModel = new UserModel
                {
                    UserId = entity.EmailId,
                    UserRole = "Parent",
                    Password = "Welcome1@",
                    Name = entity.ParentName,
                    SchoolId = entity.SchoolBranchId.Value,
                    ClassId = entity.ClassId,
                    SectionId = entity.SectionId,
                    EmailId = entity.EmailId,
                    ContactNumber = entity.ParentMobileNo                    
                };
                securitySVC.Save(userModel);
            }
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
