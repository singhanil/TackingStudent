using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentTracking.Application.Models
{
    public class HolidayModel : ModelBase
    {
        
        public int Id { get; set; }
        public string title { get; set; }
        public int SchoolId { get; set; }
        public System.DateTime start { get; set; }
        public System.DateTime end { get; set; }
        public string EventType { get; set; }

    }
}
