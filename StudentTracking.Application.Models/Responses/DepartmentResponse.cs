using StudentTracking.Application.Models.Models;
using StudentTracking.Domain;
using System.Collections.Generic;

namespace StudentTracking.Application.Models.Responses
{
    public class DepartmentResponse: ServiceResponse
    {
        public IEnumerable<DepartmentModel> Departments { get; set; }
    }
}
