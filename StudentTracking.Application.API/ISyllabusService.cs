using StudentTracking.Application.Models;
using System.Collections.Generic;

namespace StudentTracking.Application.API
{
    public interface ISyllabusService
    {
        IEnumerable<SyllabusModel> Get(int schoolId, int classId, int sectionId);
        void Save(SyllabusModel model, bool isCommit = true);
        void Save(IEnumerable<SyllabusModel> models);
    }
}
