using StudentTracking.Application.API;
using StudentTracking.Application.Models;
using StudentTracking.Data;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentTracking.Application.Main
{
    public class AttendenceReportService : IAttendenceReport
    {
        private StudentTrackingContext _dbContext = null;
        private string _baseSQLQuery = @"select sd.StudentId as StudentId, sd.StudentName as StudentName, c.Name as Class, s.name as Section, 
                                                                                CASE WHEN sa.IpAddress IS NULL THEN 'Absent' ELSE 'Present'  END AS Attendence 
                                                                                from class c, section s, TagDetails td, StudentDetail sd Left JOIN 
                                                                                (select * from StudentAttendance where InTime > getdate()-1) sa 
                                                                                on (select tagid from TagDetails where id=sd.PrimaryTagId) = sa.PrimaryTagId
                                                                                where td.ID = sd.PrimaryTagId and sd.classId = c.id and sd.sectionid = s.id";
        private string _baseMonthlySQLQuery = @"select mr.id, mr.TagId, 
                                                        CASE WHEN mr.TagId IS NULL THEN 0 ELSE mr.Count  END AS TotalPresentDays, 
                                                        sd.StudentId, sd.StudentName, C.Name as Class, S.Name as Section from StudentDetail sd, Class c, Section s, 
                                                        (select sd.ID as Id, SA.PRIMARYTAGID as TagId,  Count(*) AS COUNT
                                                        from TagDetails td, StudentDetail sd Left JOIN 
                                                        (select * from StudentAttendance where datepart(yyyy, intime) = datepart(yyyy, getdate())
                                                        and datepart(mm, intime) = datepart(mm, getdate())) sa 
                                                        on (select tagid from TagDetails where id=sd.PrimaryTagId) = sa.PrimaryTagId
                                                        where td.ID = sd.PrimaryTagId group by sd.ID, SA.PRIMARYTAGID) as mr
                                                        where sd.id = mr.id and sd.classid = c.id and sd.sectionid=s.id";

        public AttendenceReportService(StudentTrackingContext cntx)
        {
            this._dbContext = cntx;
        }

        public IEnumerable<DailyAttendenceModel> GetDailyReport(int schoolId)
        {
            string sqlQuery = string.Format("{0} and sd.schoolbranchid = @p0", _baseSQLQuery);

            return this._dbContext.Database.SqlQuery<DailyAttendenceModel>(sqlQuery, new SqlParameter("p0", schoolId)).ToList();
        }
        public IEnumerable<DailyAttendenceModel> GetDailyReport(int schoolId, int classId, int sectionId)
        {
            string sqlQuery = string.Format("{0} and sd.schoolbranchid = @p0 and sd.ClassId = @p1 and sd.SectionId = @p2", _baseSQLQuery);

            return this._dbContext.Database.SqlQuery<DailyAttendenceModel>(sqlQuery,
                new SqlParameter("p0", schoolId),
                new SqlParameter("p1", classId),
                new SqlParameter("p2", sectionId)).ToList();
        }

        public IEnumerable<MonthlyAttendenceModel> GetMonthlyReport(int schoolId)
        {
            string sqlQuery = string.Format("{0} and sd.schoolbranchid = @p0", _baseMonthlySQLQuery);
            int totalDays = __getWorkingDays(new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1), DateTime.Now, new DateTime[] { });

            var monthlyReports = this._dbContext.Database.SqlQuery<MonthlyAttendenceModel>(sqlQuery,
                new SqlParameter("p0", schoolId)).ToList();


            monthlyReports.ForEach(r =>
            {
                r.TotalSchoolDays = totalDays;
                r.TotalAbsentDays = r.TotalSchoolDays - r.TotalPresentDays;
            });
            return monthlyReports;
        }


        public IEnumerable<MonthlyAttendenceModel> GetMonthlyReport(int schoolId, int classId, int sectionId)
        {
            if (classId == 0 || sectionId == 0)
                return GetMonthlyReport(schoolId);

            string sqlQuery = string.Format("{0} and sd.schoolbranchid = @p0 and sd.ClassId = @p1 and sd.SectionId = @p2", _baseMonthlySQLQuery);
            int totalDays = __getWorkingDays(new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1), DateTime.Now, new DateTime[]{});

            var monthlyReports = this._dbContext.Database.SqlQuery<MonthlyAttendenceModel>(sqlQuery,
                new SqlParameter("p0", schoolId),
                new SqlParameter("p1", classId),
                new SqlParameter("p2", sectionId)).ToList();


            monthlyReports.ForEach(r =>
            {
                r.TotalSchoolDays = totalDays;
                r.TotalAbsentDays = r.TotalSchoolDays - r.TotalPresentDays;
            });
            
            /*var monthlyReports = new List<MonthlyAttendenceModel>();
            monthlyReports.Add(new MonthlyAttendenceModel { StudentId = "CHS2016001", StudentName = "Rahul Gupta", Class = "II", Section = "C", TotalSchoolDays = 20, TotalPresentDays = 20, TotalAbsentDays = 0 });
            monthlyReports.Add(new MonthlyAttendenceModel { StudentId = "CHS2015012", StudentName = "Rajesh Verma", Class = "II", Section = "C", TotalSchoolDays = 20, TotalPresentDays = 18, TotalAbsentDays = 2 });
            monthlyReports.Add(new MonthlyAttendenceModel { StudentId = "CHS2015013", StudentName = "Akash Lahoti", Class = "II", Section = "C", TotalSchoolDays = 20, TotalPresentDays = 15, TotalAbsentDays = 5 });
            */
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

        private void _getDailyReport(int schoolId, int classId, int sectionId)
        {
            var result = this._dbContext.Database.SqlQuery<DailyAttendenceModel>(@"select sd.StudentId as StudentId, sd.StudentName as StudentName, c.Name as Class, s.name as Section, ISNULL(sa.IpAddress, 'N') AS Attendence 
                                                                                from class c, section s, TagDetails td, StudentDetail sd Left JOIN 
                                                                                (select * from StudentAttendance where InTime > getdate()-1) sa 
                                                                                on sd.PrimaryTagId = sa.PrimaryTagId
                                                                                where
                                                                                td.ID = sd.PrimaryTagId 
                                                                                and sd.classId = c.id
                                                                                and sd.sectionid = s.id
                                                                                and sd.schoolbranchid = @p0
                                                                                and sd.ClassId = @p1
                                                                                and sd.SectionId = @p2", schoolId, classId, sectionId );
        }

        private int __getWorkingDays(DateTime fromDate, DateTime toDate, DateTime[] holidayList)
        {
            fromDate = fromDate.Date;
            toDate = toDate.Date;

            if (fromDate > toDate)
                throw new ArgumentException("Incorrect last day " + toDate);

            TimeSpan span = toDate - fromDate;
            int businessDays = span.Days + 1;
            int fullWeekCount = businessDays / 7;
            // find out if there are weekends during the time exceedng the full weeks
            if (businessDays > fullWeekCount * 7)
            {
                // we are here to find out if there is a 1-day or 2-days weekend
                // in the time interval remaining after subtracting the complete weeks
                int firstDayOfWeek = fromDate.DayOfWeek == DayOfWeek.Sunday ? 7 : (int)fromDate.DayOfWeek;
                int lastDayOfWeek = toDate.DayOfWeek == DayOfWeek.Sunday ? 7 : (int)toDate.DayOfWeek;

                if (lastDayOfWeek < firstDayOfWeek)
                    lastDayOfWeek += 7;
                if (firstDayOfWeek <= 6)
                {
                    if (lastDayOfWeek >= 7)// Both Saturday and Sunday are in the remaining time interval
                        businessDays -= 2;
                    else if (lastDayOfWeek >= 6)// Only Saturday is in the remaining time interval
                        businessDays -= 1;
                }
                else if (firstDayOfWeek <= 7 && lastDayOfWeek >= 7)// Only Sunday is in the remaining time interval
                    businessDays -= 1;
            }

            // subtract the weekends during the full weeks in the interval
            businessDays -= fullWeekCount + fullWeekCount;

            // subtract the number of bank holidays during the time interval
            foreach (DateTime bankHoliday in holidayList)
            {
                DateTime bh = bankHoliday.Date;
                if (fromDate <= bh && bh <= toDate)
                    --businessDays;
            }

            return businessDays;
        }
    }
}
