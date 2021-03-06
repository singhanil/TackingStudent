﻿using StudentTracking.Application.Models;
using StudentTracking.Application.ViewModels;
using System.Collections.Generic;

namespace StudentTracking.Application.API
{
    public interface ITimeTable
    {
        IEnumerable<TimeTableVM> FindAll(int schoolId, int classId, int sectionId);
        TimeTableModel Save(int schoolId, int classId, int sectionId, IEnumerable<TimeTableVM> list);
        bool SaveBulk(IEnumerable<TimeTableModel> list);
        IEnumerable<SubjectModel> GetSubjects(int schoolId);
        IEnumerable<LectureModel> GetLectures(int schoolId);
        IEnumerable<TimeTableMobileVM> Get(int schoolId, int classId, int sectionId, string day);
    }
}
