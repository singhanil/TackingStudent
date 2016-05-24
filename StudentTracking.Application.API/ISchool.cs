using StudentTracking.Application.Models;
using System.Collections.Generic;

namespace StudentTracking.Application.API
{
    public interface ISchool
    {
        IEnumerable<SchoolModel> GetAll();
        SchoolModel Get(int id);
        int Save(SchoolModel school);
        bool Delete(int id);
    }
}
