using StudentTracking.Application.API;
using StudentTracking.Application.Models;
using StudentTracking.Data;
using System;
using System.Collections.Generic;
using StudentTracking.Application.Main.Extensions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using StudentTracking.Application.Models.Models;

namespace StudentTracking.Application.Main
{
    public class StaffService : IStaffService
    {
        private StudentTrackingContext _dbContext = null;
        public StaffService(StudentTrackingContext cntx)
        {
            this._dbContext = cntx;
        }

        public IEnumerable<StaffModel> GetAll(int schoolId)
        {
            var entities = this._dbContext.StaffDetails.Where(s => s.SchoolId == schoolId).ToList();

            if (null != entities && entities.Count > 0)
                return entities.MapAsCollection<StaffDetail, StaffModel>();

            return new List<StaffModel>();
        }

        public IEnumerable<DepartmentModel> GetDepartment()
        {
            var entities = this._dbContext.Departments.ToList();

            if (null != entities && entities.Count > 0)
                return entities.MapAsCollection<Department, DepartmentModel>();

            return new List<DepartmentModel>();
        }

        public IEnumerable<StaffModel> GetByDepartment(int schoolId, int deptId)
        {
            var entities = this._dbContext.StaffDetails.Where(s => s.SchoolId == schoolId && s.DepartmentId == deptId).ToList();

            if (null != entities && entities.Count > 0)
                return entities.MapAsCollection<StaffDetail, StaffModel>();

            return new List<StaffModel>();
        }
        public StaffModel Get(int staffId)
        {
            var entity = this._dbContext.StaffDetails.Where(s => s.ID == staffId).FirstOrDefault();

            if (null != entity)
                return entity.MapAs<StaffDetail, StaffModel>();

            return null;
        }

        public StaffModel Save(StaffModel model)
        {
            var isNew = true;

            var entity = this._dbContext.StaffDetails.Where(s => s.ID == model.ID).FirstOrDefault();

            if (null != entity)
                isNew = false;

            entity = _populateValues(entity, model);
            if (isNew)
                this._dbContext.StaffDetails.Add(entity);
            else
                this._dbContext.Entry<StaffDetail>(entity).State = EntityState.Modified;

            this._dbContext.SaveChanges();

            
            //Create staff account

            var securitySVC = new SecurityService(this._dbContext);
            var userModel = new UserModel
            {
                UserId = entity.StaffId,
                UserRole = (entity.DepartmentId == 5) ? "Teacher" : "Staff",
                Password = "Welcome1@",
                Name = entity.StaffName,
                SchoolId = entity.SchoolId,
                ClassId = entity.ClassId,
                SectionId = entity.SectionId,
                EmailId = entity.ReportingEmailId,
                ContactNumber = entity.StaffMobileNo
            };
            securitySVC.Save(userModel);

            return entity.MapAs<StaffDetail, StaffModel>();
        }

        private StaffDetail _populateValues(StaffDetail entity, StaffModel model)
        {
            if (null == entity)
            {
                entity = new StaffDetail();

                entity.ID = model.ID;
                entity.StaffId = model.StaffId;
                entity.SchoolId = model.SchoolId;
            }
            entity.ClassId = model.ClassId;
            entity.DepartmentId = model.DepartmentId;
            entity.PrimaryTagId = model.PrimaryTagId;
            entity.ReportingEmailId = model.ReportingEmailId;
            entity.SectionId = model.SectionId;
            entity.StaffMobileNo = model.StaffMobileNo;
            entity.StaffName = model.StaffName;
            entity.Address1 = model.Address1;
            entity.Address2 = model.Address2;
            entity.City = model.City;
            entity.Country = model.Country;
            entity.DateOfBirthh = model.DateOfBirthh;
            entity.DepartmentId = model.DepartmentId;
            entity.Gender = model.Gender;
            entity.StateId = model.StateId;
            entity.ZipCode = model.ZipCode;

            return entity;
        }
    }
}
