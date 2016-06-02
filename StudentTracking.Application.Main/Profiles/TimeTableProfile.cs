using AutoMapper;
using StudentTracking.Application.Models;
using StudentTracking.Data;

namespace StudentTracking.Application.Main.Profiles
{
    public class TimeTableProfile : ProfileBase
    {
        protected override void Configure()
        {
            Mapper.CreateMap<TimeTable, TimeTableModel>()
                .ForMember(tt => tt.ClassName, ttm => ttm.MapFrom(src => src.LectureDuration.Duration))
                .ForMember(tt => tt.SectionName, ttm => ttm.MapFrom(src => src.Section.Name))
                .ForMember(tt => tt.LectureDetail, ttm => ttm.MapFrom(src => src.LectureDuration.Duration))
                .ForMember(tt => tt.Subject, ttm => ttm.MapFrom(src => src.Subject.SubjectName));
                   
            Mapper.AssertConfigurationIsValid(typeof(TimeTableProfile).Name);
        }
    }
}
