using StudentTracking.Data;
using StudentTracking.Domain;
using System.Collections.Generic;

namespace StudentTracking.Application.Models.Responses
{
    public class SchoolResponse : ServiceResponse
    {
        public SchoolModel School { get; set; }
    }

    public class AllSchoolsResponse : ServiceResponse
    {
        public IEnumerable<SchoolModel> Schools { get; set; }
    }

    public class AllHolidayResponse : ServiceResponse
    {
        public IEnumerable<Holiday> HolidayList { get; set; }
    }

    public class CalendarResponse : ServiceResponse
    {
        public IEnumerable<Holiday> eventlist { get; set; }
    }

    public class ImportantLinkResponse : ServiceResponse
    {
        public IEnumerable<ImpotantLinkModel> ImportantLinks { get; set; }
    }
}
