using StudentTracking.Domain;
using System.Collections.Generic;

namespace StudentTracking.Application.Models.Responses
{
    public class TTCommonResponse : ServiceResponse
    {
        public IEnumerable<SubjectModel> Subjects { get; set; }
        public IEnumerable<LectureModel> Lectures { get; set; }
    }
}
