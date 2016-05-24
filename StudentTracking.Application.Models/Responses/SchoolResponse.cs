using StudentTracking.Domain;
using System.Collections.Generic;

namespace StudentTracking.Application.Models.Responses
{
    public class SchoolResponse : ServiceResponse
    {
        public SchoolModel School { get; set; }
    }

    public class AllSchoolsResponse : ServiceResponse
    {
        public IEnumerable<SchoolModel> Schools { get; set; }
    }
}
