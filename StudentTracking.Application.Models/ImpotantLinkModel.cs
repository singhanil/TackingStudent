using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentTracking.Application.Models
{
    public class ImpotantLinkModel : ModelBase
    {
        public int Id { get; set; }
        public int SchoolId { get; set; }
        public string LinkTitle { get; set; }
        public string LinkUrl { get; set; }
    }
}
