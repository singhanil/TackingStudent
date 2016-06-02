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
    
    public partial class SyllabusDetail
    {
        public SyllabusDetail()
        {
            this.StudentResults = new HashSet<StudentResult>();
            this.SyllabusTrackings = new HashSet<SyllabusTracking>();
        }
    
        public int ID { get; set; }
        public int ClassId { get; set; }
        public string Subject { get; set; }
        public string Semester { get; set; }
        public string Details { get; set; }
        public int TotalMarks { get; set; }
    
        public virtual Class Class { get; set; }
        public virtual ICollection<StudentResult> StudentResults { get; set; }
        public virtual ICollection<SyllabusTracking> SyllabusTrackings { get; set; }
    }
}
