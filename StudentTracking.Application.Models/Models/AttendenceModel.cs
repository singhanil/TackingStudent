﻿
using System.Collections.Generic;
namespace StudentTracking.Application.Models
{
    public class AttendenceModel : ModelBase
    {
        public string StudentId { get; set; }
        public string StudentName { get; set; }
        public string Class { get; set; }
        public string Section { get; set; }
    }

    public class DailyAttendenceModel : AttendenceModel
    {
        public string Attendence { get; set; }
    }

    public class MonthlyAttendenceModel : AttendenceModel
    {
        public string Month { get; set; }
        public int TotalSchoolDays { get; set; }
        public int TotalPresentDays { get; set; }
        public int TotalAbsentDays { get; set; }
    }

    public class MonthlyAttendenceVM: AttendenceModel
    {
        public IEnumerable<MonthlyAttendenceModel> Attendance { get; set; }
    }
}
