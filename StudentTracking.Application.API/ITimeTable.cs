using StudentTracking.Application.Models;
using System.Collections.Generic;

namespace StudentTracking.Application.API
{
    public interface ITimeTable
    {
        IEnumerable<TimeTableModel> FindAll(int classId, int sectionId);
        TimeTableModel Save(TimeTableModel model);
    }
}
