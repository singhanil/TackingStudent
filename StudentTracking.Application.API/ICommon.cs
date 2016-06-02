using StudentTracking.Application.Models;
using StudentTracking.Data;
using System.Collections.Generic;

namespace StudentTracking.Application.API
{
    public interface ICommon
    {
        IEnumerable<Class> GetAllClasses();
        IEnumerable<Section> GetAllSections();
        IEnumerable<TagDetail> GetAllTags();
        IEnumerable<Country> GetAllCountries();
        IEnumerable<State> GetAllStates();
        IEnumerable<StateModel> FindStates(string countyCode);
    }
}
