﻿using StudentTracking.Application.API;
using StudentTracking.Application.Main.Extensions;
using StudentTracking.Application.Models;
using StudentTracking.Data;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace StudentTracking.Application.Main
{
    public class SyllabusService : ISyllabusService
    {
        private StudentTrackingContext _dbContext;
        public SyllabusService(StudentTrackingContext cntx)
        {
            this._dbContext = cntx;
        }

        public IEnumerable<SyllabusModel> Get(int schoolId, int classId, int sectionId)
        {
            var entities = this._dbContext.SyllabusDetails.Where(s => s.SchoolId == schoolId && s.ClassId == classId && s.SectionId == sectionId).ToList();
            if (entities.Count > 0)
                return entities.MapAsCollection<SyllabusDetail, SyllabusModel>();

            return new List<SyllabusModel>();
        }

        public void Save(SyllabusModel model, bool isCommit = true)
        {
            bool isNew = true;

            var entity = this._dbContext.SyllabusDetails.Where(s => s.ID == model.ID).FirstOrDefault();

            if (null == entity)
                isNew = false;

            entity = __populateDetails(entity, model);
            if (isNew)
                this._dbContext.SyllabusDetails.Add(entity);
            else
                this._dbContext.Entry<SyllabusDetail>(entity).State = EntityState.Modified;

            if (isCommit)
                this._dbContext.SaveChanges();
        }

        public void Save(IEnumerable<SyllabusModel> models)
        {
            foreach (var model in models)
                Save(model, false);

            this._dbContext.SaveChanges();
        }
        
        private SyllabusDetail __populateDetails(SyllabusDetail entity, SyllabusModel model)
        {
            entity.ClassId = model.ClassId;
            entity.Details = model.Detail;
            entity.SchoolId = model.SchoolId;
            entity.SectionId = model.SectionId;
            entity.Semester = model.Semester;
            entity.SubjectId = model.SubjectId;
            entity.TotalMarks = model.TotalMarks;
            return entity;
        }
        
    }
}
