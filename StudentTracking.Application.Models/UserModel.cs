using System;

namespace StudentTracking.Application.Models
{
    public class UserModel : ModelBase
    {
        public string UserId { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string UserRole { get; set; }
        public int SchoolId { get; set; }
        public int? ClassId { get; set; }
        public int? SectionId { get; set; }
        public string EmailId { get; set; }
        public string ContactNumber { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}
