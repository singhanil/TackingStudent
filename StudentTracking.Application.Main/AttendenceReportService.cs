using StudentTracking.Application.API;
using StudentTracking.Application.Models;
using StudentTracking.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentTracking.Application.Main
{
    public class AttendenceReportService : IAttendenceReport
    {
        private StudentTrackingContext _dbContext = null;

        public AttendenceReportService(StudentTrackingContext cntx)
        {
            this._dbContext = cntx;
        }

        public IEnumerable<DailyAttendenceModel> GetDailyReport()
        {
            var dailyReports = new List<DailyAttendenceModel>();
            dailyReports.Add(new DailyAttendenceModel { StudentId = "CHS2016001", StudentName = "Rahul Gupta", Class = "II", Section = "C", Attendence = "Present" });
            dailyReports.Add(new DailyAttendenceModel { StudentId = "CHS2015012", StudentName = "Rajesh Verma", Class = "II", Section = "C", Attendence = "Absent" });
            dailyReports.Add(new DailyAttendenceModel { StudentId = "CHS2015013", StudentName = "Akash Lahoti", Class = "II", Section = "C", Attendence = "Present" });
            return dailyReports;
        }

        public IEnumerable<MonthlyAttendenceModel> GetMonthlyReport()
        {
            var monthlyReports = new List<MonthlyAttendenceModel>();
            monthlyReports.Add(new MonthlyAttendenceModel { StudentId = "CHS2016001", StudentName = "Rahul Gupta", Class = "II", Section = "C", TotalSchoolDays = 20, TotalPresentDays = 20, TotalAbsentDays = 0 });
            monthlyReports.Add(new MonthlyAttendenceModel { StudentId = "CHS2015012", StudentName = "Rajesh Verma", Class = "II", Section = "C", TotalSchoolDays = 20, TotalPresentDays = 18, TotalAbsentDays = 2 });
            monthlyReports.Add(new MonthlyAttendenceModel { StudentId = "CHS2015013", StudentName = "Akash Lahoti", Class = "II", Section = "C", TotalSchoolDays = 20, TotalPresentDays = 15, TotalAbsentDays = 5 });
            return monthlyReports;
        }

        public IEnumerable<MonthlyAttendenceModel> GetYearlyReport()
        {
            var monthlyReports = new List<MonthlyAttendenceModel>();
            monthlyReports.Add(new MonthlyAttendenceModel { StudentId = "CHS2016001", StudentName = "Rahul Gupta", Class = "II", Section = "C", TotalSchoolDays = 190, TotalPresentDays = 180, TotalAbsentDays = 10 });
            monthlyReports.Add(new MonthlyAttendenceModel { StudentId = "CHS2015012", StudentName = "Rajesh Verma", Class = "II", Section = "C", TotalSchoolDays = 190, TotalPresentDays = 189, TotalAbsentDays = 1 });
            monthlyReports.Add(new MonthlyAttendenceModel { StudentId = "CHS2015013", StudentName = "Akash Lahoti", Class = "II", Section = "C", TotalSchoolDays = 190, TotalPresentDays = 175, TotalAbsentDays = 15 });
            return monthlyReports;
        }
    }
}
