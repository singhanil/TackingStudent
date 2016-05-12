using StudentTracking.Data;
using System.Collections.Generic;

namespace StudentTracking.Domain
{
    public class SchoolResponse : ServiceResponse
    {
        public School School { get; set; }
    }

    public class AllSchoolsResponse : ServiceResponse
    {
        public IEnumerable<School> Schools { get; set; }
    }
}
