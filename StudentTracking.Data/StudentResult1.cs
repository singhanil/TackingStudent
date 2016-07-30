//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace StudentTracking.Data
{
    using System;
    using System.Collections.Generic;
    
    public partial class StudentResult1
    {
        public int ID { get; set; }
        public int SchoolId { get; set; }
        public int StudentId { get; set; }
        public int SemesterId { get; set; }
        public int SubjectId { get; set; }
        public Nullable<decimal> MarksObtained { get; set; }
        public string Remark { get; set; }
    
        public virtual School School { get; set; }
        public virtual StudentDetail StudentDetail { get; set; }
        public virtual SemesterDetail SemesterDetail { get; set; }
        public virtual Subject Subject { get; set; }
    }
}