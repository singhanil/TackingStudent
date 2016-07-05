using StudentTracking.Domain;
using System.Collections.Generic;

namespace StudentTracking.Application.Models.Responses
{
    public class SyllabusResponse : ServiceResponse
    {
        public IEnumerable<SyllabusModel> Syllabus { get; set; }
    }
}
