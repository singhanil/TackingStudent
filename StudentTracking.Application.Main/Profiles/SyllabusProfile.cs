using AutoMapper;
using StudentTracking.Application.Models;
using StudentTracking.Data;

namespace StudentTracking.Application.Main.Profiles
{
    public class SyllabusProfile : ProfileBase
    {
        protected override void Configure()
        {
            Mapper.CreateMap<SyllabusDetail, SyllabusModel>()
                .ForMember(tt => tt.ClassName, ttm => ttm.MapFrom(src => src.Class.Name))
                .ForMember(tt => tt.SubjectName, ttm => ttm.MapFrom(src => src.Subject.SubjectName));

            Mapper.AssertConfigurationIsValid(typeof(SyllabusProfile).Name);
        }
    }
}
