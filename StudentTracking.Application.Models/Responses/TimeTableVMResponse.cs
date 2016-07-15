using StudentTracking.Application.ViewModels;
using StudentTracking.Domain;
using System.Collections.Generic;

namespace StudentTracking.Application.Models.Responses
{
    public class TimeTableVMResponse : ServiceResponse
    {
        public IEnumerable<TimeTableMobileVM> Monday { get; set; }
        public IEnumerable<TimeTableMobileVM> Tuesday { get; set; }
        public IEnumerable<TimeTableMobileVM> Wednessday { get; set; }
        public IEnumerable<TimeTableMobileVM> Thursday { get; set; }
        public IEnumerable<TimeTableMobileVM> Friday { get; set; }

    }
}
