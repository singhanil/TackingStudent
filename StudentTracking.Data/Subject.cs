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
    
    public partial class Subject
    {
        public Subject()
        {
            this.TimeTables = new HashSet<TimeTable>();
            this.SyllabusDetails = new HashSet<SyllabusDetail>();
            this.StudentResults = new HashSet<StudentResult1>();
        }
    
        public int ID { get; set; }
        public string SubjectName { get; set; }
    
        public virtual ICollection<TimeTable> TimeTables { get; set; }
        public virtual ICollection<SyllabusDetail> SyllabusDetails { get; set; }
        public virtual ICollection<StudentResult1> StudentResults { get; set; }
    }
}
