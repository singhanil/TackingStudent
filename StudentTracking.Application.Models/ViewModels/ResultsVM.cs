using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentTracking.Application.ViewModels
{
    public class ResultsVM
    {
        public string SemesterName { get; set; }
        public IEnumerable<ResultVM> Result { get; set; }
    }

    public class ResultVM
    {
        public string Subject { get; set; }
        public int TotalMarks { get; set; }
        public string Grade { get; set; }
    }
}
