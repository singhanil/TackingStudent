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

        public IEnumerable<Class> GetAllClasses()
        {
            return this._dbContext.Classes.ToList();
        }

        public IEnumerable<Section> GetAllSections()
        {
            return this._dbContext.Sections.ToList();
        }

        public IEnumerable<TagDetail> GetAllTags()
        {
            return this._dbContext.TagDetails.ToList();
        }

        public IEnumerable<Country> GetAllCountries()
        {
            return this._dbContext.Countries.ToList();
        }

        public IEnumerable<State> GetAllStates()
        {
            return this._dbContext.States.ToList();
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
