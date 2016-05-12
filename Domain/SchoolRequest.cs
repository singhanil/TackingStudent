using StudentTracking.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class AllSchoolRequest : ServiceRequest
    {
        
    }
    public class SchoolRequest : ServiceRequest
    {
        public string ID { get; set; }
    }

    public class SaveSchoolRequest : ServiceRequest
    {
        public School School { get; set; }
    }
}
