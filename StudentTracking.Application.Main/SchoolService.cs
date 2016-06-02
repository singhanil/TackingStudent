using StudentTracking.Application.API;
using StudentTracking.Application.Main.Extensions;
using StudentTracking.Application.Models;
using StudentTracking.Data;

using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace StudentTracking.Application.Main
{
    public class SchoolService : ISchool
    {
        private StudentTrackingContext _dbContext = null;

        public SchoolService(StudentTrackingContext cntx)
        {
            this._dbContext = cntx;
        }
        public IEnumerable<SchoolModel> GetAll()
        {
            var entities = this._dbContext.Schools.ToList();
            return entities.MapAsCollection<School, SchoolModel>();
        }

        public SchoolModel Get(int id)
        {
            var entity = this._dbContext.Schools.Where(e => e.ID == id).FirstOrDefault();
            
            if (null != entity)
                return entity.MapAs<School, SchoolModel>();
            
            return null;
        }
        public SchoolModel Save(SchoolModel model)
        {
            var entity = this._dbContext.Schools.Where(e => e.ID == model.Id).FirstOrDefault();
            entity = _populateValues(entity, model);

            if (model.Id > 0)
            {
                if (null != entity)
                    this._dbContext.Entry(entity).State = EntityState.Modified;
                else
                    this._dbContext.Schools.Add(entity);
            }
            else
                this._dbContext.Schools.Add(entity);
            
            this._dbContext.SaveChanges();
            
            return entity.MapAs<School, SchoolModel>();
        }

        private School _populateValues(School entity, SchoolModel model)
        {
            if (null == entity)
            {
                entity = new School();
                entity.EmailId = model.EmailId;
                entity.OrganizationId = model.OrganizationId;
                
                entity.CreatedDate = DateTime.Now;
                entity.ModifiedDate = DateTime.Now;
            }
            entity.Address1 = model.Address1;
            entity.Address2 = model.Address2;
            entity.City = model.City;
            entity.ContactPerson = model.ContactPerson;
            entity.Country = model.Country;
            entity.Description = model.Description;
            entity.ModifiedDate = DateTime.Now;
            entity.Phone = model.Phone;
            entity.State = model.State;
            entity.ThemeId = model.ThemeId;
            entity.Title = model.Title;
            entity.Twitter = model.Twitter;
            entity.FaceBookUrl = model.FaceBookUrl;
            entity.LinkedIn = model.LinkedIn;
            entity.IsActive = model.IsActive;

            return entity;
        }
        public bool Delete(int id)
        {
            //TODO: Need to have soft delete.
            var entity = this._dbContext.Schools.Where(s => s.ID == id).FirstOrDefault();
            if (null != entity)
            {
                this._dbContext.Entry(entity).State = EntityState.Deleted;
                return true;
            }
            return false;
        }
    }
}
