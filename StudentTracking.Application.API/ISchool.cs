using StudentTracking.Application.Models;
using System.Collections.Generic;

namespace StudentTracking.Application.API
{
    public interface ISchool
    {
        IEnumerable<SchoolModel> GetAll();
        SchoolModel Get(int id);
        SchoolModel Save(SchoolModel school);
        bool Delete(int id);
        IEnumerable<Data.Holiday> GetHolidayList(int schoolId);
        IEnumerable<Data.Holiday> GetCalendarEvents(int schoolId);
        IEnumerable<ImpotantLinkModel> getImportantLinks(int schoolId);
    }
}
