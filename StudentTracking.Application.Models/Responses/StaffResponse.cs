using StudentTracking.Domain;
using System.Collections.Generic;

namespace StudentTracking.Application.Models.Responses
{
    public class StaffResponse : ServiceResponse
    {
        public StaffModel Staff { get; set; }
    }
    public class StaffsResponse : ServiceResponse
    {
        public IEnumerable<StaffModel> Staffs { get; set; }
    }
}
