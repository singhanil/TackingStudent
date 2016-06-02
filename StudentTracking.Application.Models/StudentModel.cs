using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentTracking.Application.Models
{
    public class StudentModel
    {
        public int Id { get; set; }
        public string StudentId { get; set; }
        public string ParentMobileNo { get; set; }
        public int PrimaryTagId { get; set; }
        public int SecondaryTagId { get; set; }
        public string EmailId { get; set; }
        public string StudentName { get; set; }
        public Nullable<int> SchoolBranchId { get; set; }
        public Nullable<int> ClassId { get; set; }
        public Nullable<int> SectionId { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
        public string Country { get; set; }
        public string Gender { get; set; }
        public System.DateTime DateOfBirthh { get; set; }
        public string ParentName { get; set; }

        public string PrimaryTagDetail { get; set; }
        public string SecondaryTagDetail { get; set; }
        public string SchoolName { get; set; }
        public string ClassName { get; set; }
        public string SectionName { get; set; }
    }
}
