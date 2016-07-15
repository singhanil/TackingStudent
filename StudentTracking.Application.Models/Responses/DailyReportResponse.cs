using StudentTracking.Domain;
using System.Collections.Generic;

namespace StudentTracking.Application.Models.Responses
{
    public class DailyReportResponse : ServiceResponse
    {
        public IEnumerable<DailyAttendenceModel> Reports { get; set; }
    }

    public class MonthlyReportResponse : ServiceResponse
    {
        public IEnumerable<MonthlyAttendenceModel> Reports { get; set; }
    }

    public class MonthlyReportVMResponse : ServiceResponse
    {
        public MonthlyAttendenceVM Attendence { get; set; }
    }
}
