using StudentTracking.Data;
using StudentTracking.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentTracking.Domain
{
    public class StudentResponse : ServiceResponse
    {
        public StudentDetail Student { get; set; }
    }

    public class AllStudentResponse : ServiceResponse
    {
        public IEnumerable<StudentDetail> Students { get; set; }
    }
}
