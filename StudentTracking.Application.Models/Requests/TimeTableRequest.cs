using System.Collections.Generic;

namespace StudentTracking.Application.Models.Requests
{
    public class TimeTableRequest: ServiceRequest
    {
        public int SchoolId { get; set; }
        public int ClassId { get; set; }
        public int SectionId { get; set; }
        public IEnumerable<TimeTableVM> TimeTable { get; set; }
    }

    public class BulkTimeTableRequest : ServiceRequest
    {
        public IEnumerable<TimeTableModel> TimeTables { get; set; }
    }
}
