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
    
    public partial class Notification
    {
        public int MessageId { get; set; }
        public int SchoolId { get; set; }
        public Nullable<int> ClassId { get; set; }
        public Nullable<int> SectionId { get; set; }
        public string StudentId { get; set; }
        public string MessageType { get; set; }
        public string Subject { get; set; }
        public string MessageText { get; set; }
        public string FilePath { get; set; }
        public System.DateTime CreatedDate { get; set; }
    }
}
