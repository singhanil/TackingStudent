using StudentTracking.Application.API;
using StudentTracking.Application.Main.Extensions;
using StudentTracking.Application.Models;
using StudentTracking.Data;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace StudentTracking.Application.Main
{
    public class TimeTableService : ITimeTable
    {
        private StudentTrackingContext _dbContext;
        public TimeTableService(StudentTrackingContext cntx)
        {
            this._dbContext = cntx;
        }
        public IEnumerable<TimeTableModel> FindAll(int classId, int sectionId)
        {
            var entities =  this._dbContext.TimeTables.Where(tt => tt.ClassId == classId && tt.SectionId == sectionId).ToList();
            if (null != entities)
                return entities.MapAsCollection<TimeTable, TimeTableModel>();

            return null;
        }
        
        public TimeTableModel Save(TimeTableModel model)
        {
            var entity = this._dbContext.TimeTables.Where(tt => tt.ID == model.ID).FirstOrDefault();

            entity = __populateValues(entity, model);
            if (entity.ID > 0)
                this._dbContext.Entry(entity).State = EntityState.Modified;
            else
                this._dbContext.TimeTables.Add(entity);

            this._dbContext.SaveChanges();

            return entity.MapAs<TimeTable, TimeTableModel>();
        }

        public bool SaveBulk(IEnumerable<TimeTableModel> list)
        {
            foreach(TimeTableModel model in list)
            {
                this.Save(model);
            }
            return true;
        }
        private TimeTable __populateValues(TimeTable entity, TimeTableModel model)
        {
            if(null == entity)
            {
                entity = new TimeTable();
            }
            entity.ClassId = model.ClassId;
            entity.SectionId = model.SectionId;
            entity.LectureId = model.LectureId;
            entity.SubjectId = model.SubjectId;
            entity.Days = model.Days;

            return entity;
        }
    }
}
