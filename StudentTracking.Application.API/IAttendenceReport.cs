using StudentTracking.Application.Models;
using System.Collections.Generic;

namespace StudentTracking.Application.API
{
    public interface IAttendenceReport
    {
        IEnumerable<DailyAttendenceModel> GetDailyReport();
        IEnumerable<MonthlyAttendenceModel> GetMonthlyReport();
        IEnumerable<MonthlyAttendenceModel> GetYearlyReport();
    }
}
