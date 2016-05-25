using StudentTracking.Application.API;
using StudentTracking.Application.Models;
using StudentTracking.Data;
using StudentTracking.Application.Main.Extensions;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using AutoMapper;
using System;

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
            var entity = this._dbContext.Schools.Where(e => e.Id == id).FirstOrDefault();
            
            if (null != entity)
                return entity.MapAs<School, SchoolModel>();
            
            return null;
        }
        public SchoolModel Save(SchoolModel model)
        {
            var entity = this._dbContext.Schools.Where(e => e.Id == model.Id).FirstOrDefault();
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
                entity = new School();

            var entityResponse = model.MapAs<School>();
            entityResponse.Id = entity.Id;
            entityResponse.CreatedDate = entity.CreatedDate;
            entityResponse.ModifiedDate = DateTime.Now;

            return entityResponse;
        }
        public bool Delete(int id)
        {
            //TODO: Need to have soft delete.
            var entity = this._dbContext.Schools.Where(s => s.Id == id).FirstOrDefault();
            if (null != entity)
            {
                this._dbContext.Entry(entity).State = EntityState.Deleted;
                return true;
            }
            return false;
        }
    }
}
