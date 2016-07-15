using StudentTracking.Application.ViewModels;
using StudentTracking.Domain;
using System.Collections.Generic;

namespace StudentTracking.Application.Responses
{
    public class ResultVMResponse: ServiceResponse
    {
        public IEnumerable<ResultsVM> Results { get; set; }
    }
}
