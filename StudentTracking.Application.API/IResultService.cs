using StudentTracking.Application.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentTracking.Application.API
{
    public interface IResultService
    {
        IEnumerable<ResultsVM> Get(int schoolId, string studentId);
    }
}
