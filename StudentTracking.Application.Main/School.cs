using StudentTracking.Application.API;
using StudentTracking.Data;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace StudentTracking.Application.Main
{
    public class School : ISchool
    {
        private StudentTrackingContext _dbContext = null;

        public School(StudentTrackingContext cntx)
        {
            this._dbContext = cntx;
        }
        public IEnumerable<Data.School> GetAll()
        {
            return this._dbContext.Schools.ToList();
        }
        public Data.School Get(int id)
        {
            return this._dbContext.Schools.Where(e => e.Id == id).FirstOrDefault();
        }
        public int Save(Data.School school)
        {
            if (school.Id > 0)
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
            
            return school.Id;
        }

        public bool Delete(int id)
        {
            return false;
        }
    }
}
