using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentTracking.Application.Models
{
    public class DocumentModel : ModelBase
    {
        public int ID { get; set; }
        public string DocumentName { get; set; }
        public string UserId { get; set; }
        public int SchoolId { get; set; }
        public int ClassId { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public System.DateTime UpdatedDate { get; set; }
    }
}
