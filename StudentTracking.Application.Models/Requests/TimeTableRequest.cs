using System.Collections.Generic;

namespace StudentTracking.Application.Models.Requests
{
    public class TimeTableRequest: ServiceRequest
    {
        public TimeTableModel TimeTable { get; set; }
    }

    public class BulkTimeTableRequest : ServiceRequest
    {
        public IEnumerable<TimeTableModel> TimeTables { get; set; }
    }
}
