using StudentTracking.Application.Models;
using System.Collections.Generic;

namespace StudentTracking.Application.API
{
    public interface IAttendenceReport
    {
        IEnumerable<DailyAttendenceModel> GetDailyReport(int schoolId);
        IEnumerable<DailyAttendenceModel> GetDailyReport(int schoolId, int classId, int sectionId);
        IEnumerable<MonthlyAttendenceModel> GetMonthlyReport(int schoolId);
        IEnumerable<MonthlyAttendenceModel> GetMonthlyReport(int schoolId, int classId, int sectionId);
        IEnumerable<MonthlyAttendenceModel> GetYearlyReport();
        MonthlyAttendenceVM GetMonthlyReportByStudent(int schoolId, string studentId);
    }
}
