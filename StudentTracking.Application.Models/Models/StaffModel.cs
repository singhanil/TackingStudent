using System;

namespace StudentTracking.Application.Models
{
    public class StaffModel : ModelBase
    {
        public int ID { get; set; }
        public string StaffId { get; set; }
        public string StaffName { get; set; }
        public int DepartmentId { get; set; }
        public int PrimaryTagId { get; set; }
        public string ReportingEmailId { get; set; }
        public string StaffMobileNo { get; set; }
        public Nullable<int> ClassId { get; set; }
        public Nullable<int> SectionId { get; set; }
        public int SchoolId { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string StateId { get; set; }
        public string ZipCode { get; set; }
        public string Country { get; set; }
        public string Gender { get; set; }
        public System.DateTime DateOfBirthh { get; set; }

        public string DepartmentName { get; set; }
        public string PrimaryTagDetail { get; set; }
        public string CountryName { get; set; }
        public string SchoolName { get; set; }
        public string ClassName { get; set; }
        public string SectionName { get; set; }
    }
}
