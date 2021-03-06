﻿using StudentTracking.Domain;
using System.Collections.Generic;

namespace StudentTracking.Application.Models.Responses
{
    public class TimeTableResponse : ServiceResponse
    {
        public IEnumerable<TimeTableVM> TimeTables { get; set; }
    }
}
