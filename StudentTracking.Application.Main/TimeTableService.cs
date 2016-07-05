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
        public IEnumerable<TimeTableVM> FindAll(int schoolId, int classId, int sectionId)
        {
            var entities =  this._dbContext.TimeTables.Where(tt => tt.ClassId == classId && tt.SectionId == sectionId).ToList().OrderBy(t=>t.LectureId);
            List<TimeTableVM> vms = new List<TimeTableVM>();

            var lectures = entities.Select(e => e.LectureId).Distinct();

            foreach(var lecture in lectures)
            {
                var lecEntities = entities.Where(e => e.LectureId == lecture).ToList();
                var vm = new TimeTableVM { Lecture = lecEntities.First().LectureDuration.Duration };
                foreach(var le in lecEntities)
                {
                    var days = le.Days.Split(',');
                    foreach(var day in days)
                    {
                        switch(day)
                        {
                            case "ALL":
                                vm.Monday = vm.Tuesday = vm.Wednessday = vm.Thursday = vm.Friday = le.Subject.SubjectName;
                                break;
                            case "M":
                            case "MO":
                                vm.Monday = le.Subject.SubjectName;
                                break;
                            case "T":
                            case "TU":
                                vm.Tuesday = le.Subject.SubjectName;
                                break;
                            case "W":
                            case "WE":
                                vm.Wednessday = le.Subject.SubjectName;
                                break;
                            case "TH":
                                vm.Thursday = le.Subject.SubjectName;
                                break;
                            case "F":
                            case "FR":
                                vm.Friday = le.Subject.SubjectName;
                                break;
                        }
                    }
                }
                vms.Add(vm);
            }
            return vms;
        }
        
        public TimeTableModel Save(int schoolId, int classId, int sectionId, IEnumerable<TimeTableVM> list)
        {
            var lectureInfo = this._dbContext.LectureDurations.ToList();
            var subjects = this._dbContext.Subjects.ToList();

            foreach(var model in list)
            {
                int lectureId = lectureInfo.Where(l=>l.Duration.Equals(model.Lecture)).FirstOrDefault().ID;

                //var entity = new TimeTable{ SchoolId = schoolId, ClassId= classId, SectionId= sectionId, LectureId = lectureId }
            }
            /*var entity = this._dbContext.TimeTables.Where(tt => tt.ID == model.Id).FirstOrDefault();

            entity = __populateValues(entity, model);
            if (entity.ID > 0)
                this._dbContext.Entry(entity).State = EntityState.Modified;
            else
                this._dbContext.TimeTables.Add(entity);

            this._dbContext.SaveChanges();

            return entity.MapAs<TimeTable, TimeTableModel>();
             * */
            return new TimeTableModel();
        }

        public bool SaveBulk(IEnumerable<TimeTableModel> list)
        {
            /*foreach(TimeTableModel model in list)
            {
                this.Save(model);
            }*/
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

        public IEnumerable<SubjectModel> GetSubjects(int schoolId)
        {
            var entities = this._dbContext.Subjects.ToList();
            if (entities.Count > 0)
                return entities.MapAsCollection<Subject, SubjectModel>();
            return new List<SubjectModel>();
        }
        public IEnumerable<LectureModel> GetLectures(int schoolId)
        {
            var entities = this._dbContext.LectureDurations.ToList();
            if (entities.Count > 0)
                return entities.MapAsCollection<LectureDuration, LectureModel>();

            return new List<LectureModel>();
        }
    }
}
