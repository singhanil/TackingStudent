using StudentTracking.Application.Models;
using StudentTracking.Application.Models.Models;
using System.Collections.Generic;

namespace StudentTracking.Application.API
{
    public interface IStaffService
    {
        IEnumerable<StaffModel> GetAll(int schoolId);
        IEnumerable<StaffModel> GetByDepartment(int schoolId, int deptId);
        StaffModel Get(int staffId);
        StaffModel Save(StaffModel model);
        IEnumerable<DepartmentModel> GetDepartment();
    }
}
