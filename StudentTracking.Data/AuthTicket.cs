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
    
    public partial class AuthTicket
    {
        public int ID { get; set; }
        public string SecurityToken { get; set; }
        public System.DateTime ExpirationDate { get; set; }
        public System.DateTime ModifiedDate { get; set; }
    }
}
