using StudentTracking.Application.API;
using StudentTracking.Application.ViewModels;
using StudentTracking.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentTracking.Application.Main
{
    public class ResultService : IResultService
    {
        private StudentTrackingContext _dbContext = null;
        public ResultService(StudentTrackingContext cntx)
        {
            this._dbContext = cntx;
        }
        public IEnumerable<ResultsVM> Get(int schoolId, string studentId)
        {
            //var entities = this._dbContext.StudentResults.Where(r=>r.)
            List<ResultsVM> results = new List<ResultsVM>();

            results.Add(new ResultsVM
            {
                SemesterName = "Semester 1",
                Result = new List<ResultVM>() { new ResultVM { Subject = "Hindi", TotalMarks = 10, Grade = "A" }, 
                new ResultVM { Subject = "English", TotalMarks = 10, Grade = "A+" },
                new ResultVM {Subject="Science", TotalMarks=10, Grade="A+" },
                new ResultVM {Subject="Math", TotalMarks=10, Grade="A" },
                new ResultVM {Subject="SST", TotalMarks=10, Grade="B" },
                new ResultVM {Subject="IT", TotalMarks=10, Grade="A+" }}
            });
            results.Add(new ResultsVM
            {
                SemesterName = "Semester 2",
                Result = new List<ResultVM>() { new ResultVM { Subject = "Hindi", TotalMarks = 10, Grade = "B" }, 
                new ResultVM { Subject = "English", TotalMarks = 10, Grade = "A" },
                new ResultVM {Subject="Science", TotalMarks=10, Grade="A+" },
                new ResultVM {Subject="Math", TotalMarks=10, Grade="A+" },
                new ResultVM {Subject="SST", TotalMarks=10, Grade="A" },
                new ResultVM {Subject="IT", TotalMarks=10, Grade="A" }}
            });
            results.Add(new ResultsVM
            {
                SemesterName = "Semester 3",
                Result = new List<ResultVM>() { new ResultVM { Subject = "Hindi", TotalMarks = 30, Grade = "A" }, 
                new ResultVM { Subject = "English", TotalMarks = 30, Grade = "A+" },
                new ResultVM {Subject="Science", TotalMarks=30, Grade="A+" },
                new ResultVM {Subject="Math", TotalMarks=30, Grade="A" },
                new ResultVM {Subject="SST", TotalMarks=30, Grade="A" },
                new ResultVM {Subject="IT", TotalMarks=30, Grade="A+" }}
            });

            return results;
        }
    }
}
