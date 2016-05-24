using StudentTracking.Application.API;
using StudentTracking.Application.Models;
using StudentTracking.Data;
using StudentTracking.Application.Main.Extensions;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using AutoMapper;

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
        public int Save(SchoolModel school)
        {
            /*if (school.Id > 0)
            {
                var entity = this._dbContext.Schools.Where(e => e.Id == school.Id).FirstOrDefault();

                if (null != entity)
                {
                    this._dbContext.Entry(school).State = EntityState.Modified;
                }
                else
                    this._dbContext.Schools.Add(school);

            }
            else
                this._dbContext.Schools.Add(school);
            
            this._dbContext.SaveChanges();
            */
            return school.Id;
        }

        public bool Delete(int id)
        {
            return false;
        }
    }
}
