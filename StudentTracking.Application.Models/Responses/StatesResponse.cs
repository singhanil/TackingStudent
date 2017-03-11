using StudentTracking.Domain;
using System.Collections.Generic;

namespace StudentTracking.Application.Models.Responses
{
    public class StatesResponse : ServiceResponse
    {
        public IEnumerable<StateModel> States { get; set; }
    }
   
}
