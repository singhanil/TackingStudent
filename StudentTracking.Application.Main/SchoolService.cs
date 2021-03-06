﻿using StudentTracking.Application.API;
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
            var isNew = false;

            var entity = this._dbContext.Schools.Where(e => e.ID == model.Id).FirstOrDefault();

            if (null == entity)
                isNew = true;

            entity = _populateValues(entity, model);

            if(isNew)
                this._dbContext.Schools.Add(entity);
            else
                this._dbContext.Entry(entity).State = EntityState.Modified;
            
            this._dbContext.SaveChanges();
            
            //Create account for school Admin
            if (isNew)
            {
                var securitySVC = new SecurityService(this._dbContext);
                var userModel = new UserModel
                {
                    UserId = entity.EmailId,
                    UserRole = "SchoolAdmin",
                    Password = "Welcome1@",
                    Name = entity.ContactPerson,
                    SchoolId = entity.ID,
                    ClassId = null,
                    SectionId = null,
                    EmailId = entity.EmailId,
                    ContactNumber = entity.Phone
                };
                securitySVC.Save(userModel);
            }
            return entity.MapAs<School, SchoolModel>();
        }

        private School _populateValues(School entity, SchoolModel model)
        {
            if (null == entity)
            {
                entity = new School();
                entity.EmailId = model.EmailId;
                entity.OrganizationId = model.OrganizationId;
                entity.BranchName = model.BranchName;
                
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
        

        public IEnumerable<Holiday> GetHolidayList(int schoolId)
        {
            return this._dbContext.Holidays.Where(e => e.SchoolId == schoolId && e.EventType != "calendar").ToList();

            //return entities.MapAsCollection<Holiday, HolidayModel>();
        }

        public IEnumerable<Holiday> GetCalendarEvents(int schoolId)
        {
            return this._dbContext.Holidays.Where(e => e.SchoolId == schoolId && e.EventType == "calendar").ToList();

            //return entities.MapAsCollection<Holiday, HolidayModel>();
        }

        public IEnumerable<ImpotantLinkModel> getImportantLinks(int schoolId)
        {
            var entities = this._dbContext.Important_Links.Where(e => e.SchoolId == schoolId).ToList();

            return (entities != null) ? entities.MapAsCollection<Important_Links, ImpotantLinkModel>() : null;
        }

        public IEnumerable<string> GetAlerts(int schoolId)
        {
            List<string> list = new List<string> { "School will repon on 17-Apr-2017", "Please collect books, uniform from schoool" };
            return list;
        }

        public string GetBlogUrl(int schoolId)
        {
            return "https://edublogs.org/";
        }

        public IEnumerable<EventModel> GetEvents(int schoolId)
        {
            int currentMonth = DateTime.Now.Month;
            var entities = this._dbContext.Holidays.Where(h => h.SchoolId == schoolId && (h.start.Month == currentMonth || h.end.Month == currentMonth));

            return (entities != null) ? entities.MapAsCollection<Holiday, EventModel>() : null;
        }
    }
}
