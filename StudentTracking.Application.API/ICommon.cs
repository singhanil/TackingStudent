using StudentTracking.Application.Models;
using StudentTracking.Data;
using System.Collections.Generic;

namespace StudentTracking.Application.API
{
    public interface ICommon
    {
        IEnumerable<ClassModel> GetAllClasses();
        IEnumerable<SubjectModel> GetAllSubjects();
        IEnumerable<SectionModel> GetAllSections();
        IEnumerable<TagDetailModel> GetAllTags();
        IEnumerable<CountryModel> GetAllCountries();
        IEnumerable<StateModel> GetAllStates();
        IEnumerable<StateModel> FindStates(string countyCode);
    }
}
