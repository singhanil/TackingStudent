using StudentTracking.Application.API;
using StudentTracking.Application.Models;
using StudentTracking.Application.Main.Extensions;
using StudentTracking.Data;
using System.Collections.Generic;
using System.Linq;

namespace StudentTracking.Application.Main
{
    public class CommonService : ICommon
    {
        StudentTrackingContext _dbContext = null;

        public CommonService(StudentTrackingContext cntx)
        {
            this._dbContext = cntx;
        }

        public IEnumerable<ClassModel> GetAllClasses()
        {
            var entities = this._dbContext.Classes.ToList();
            if (null != entities)
                return entities.MapAsCollection<Class, ClassModel>();

            return null;
        }

        public IEnumerable<SubjectModel> GetAllSubjects()
        {
            var entities = this._dbContext.Subjects.ToList();
            if (null != entities)
                return entities.MapAsCollection<Subject, SubjectModel>();

            return null;
        }

        public IEnumerable<SectionModel> GetAllSections()
        {
            var entities = this._dbContext.Sections.ToList();
            if (null != entities)
                return entities.MapAsCollection<Section, SectionModel>();

            return null;
        }

        public IEnumerable<TagDetailModel> GetAllTags()
        {
            var entities = this._dbContext.TagDetails.Where(t=>t.IsAvailable.Equals("Y")).ToList();
            if (null != entities && entities.Count > 0)
                return entities.MapAsCollection<TagDetail, TagDetailModel>();

            return new List<TagDetailModel>();
        }

        public IEnumerable<CountryModel> GetAllCountries()
        {
            var entities = this._dbContext.Countries.ToList();
            if (null != entities)
                return entities.MapAsCollection<Country, CountryModel>();

            return null;
        }

        public IEnumerable<StateModel> GetAllStates()
        {
            var entities = this._dbContext.States.ToList();
            if (null != entities)
                return entities.MapAsCollection<State, StateModel>();

            return null;
        }

        public IEnumerable<StateModel> FindStates(string countyCode)
        {
            var entities = this._dbContext.States.Where(s => s.CountryCode.Equals(countyCode)).ToList();
            if (null != entities)
                return entities.MapAsCollection<State, StateModel>();

            return null;
        }

        
    }
}
