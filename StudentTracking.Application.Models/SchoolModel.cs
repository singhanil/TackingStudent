using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentTracking.Application.Models
{
    public class SchoolModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string BranchName { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string ContactPerson { get; set; }
        public string Title { get; set; }
        public string Phone { get; set; }
        public string EmailId { get; set; }
        public string Description { get; set; }
        public string FaceBookUrl { get; set; }
        public string Twitter { get; set; }
        public string LinkedIn { get; set; }
        public int OrganizationId { get; set; }
        public int ThemeId { get; set; }
        public string IsActive { get; set; }
        public System.DateTime ModifiedDate { get; set; }

        public string OrganizationName { get; set; }
        public string ThemeDetail { get; set; }
    }
}
