using StudentTracking.Data;
using System.Collections.Generic;

namespace StudentTracking.Application.API
{
    public interface ISchool
    {
        IEnumerable<School> GetAll();
        School Get(int id);
        int Save(School school);
        bool Delete(int id);
    }
}
