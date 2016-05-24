using AutoMapper;
using StudentTracking.Application.Models;
using StudentTracking.Data;

namespace StudentTracking.Application.Main.Profiles
{
    public class StudentProfile : ProfileBase
    {
        protected override void Configure()
        {
            Mapper.CreateMap<StudentDetail, StudentModel>()
                .ForMember(sm => sm.ClassName, s => s.MapFrom(src => src.Class.Name))
                .ForMember(sm => sm.SectionName, s => s.MapFrom(src => src.Section.Name))
                .ForMember(sm => sm.SchoolName, s => s.MapFrom(src => string.Format("{0}, {1}", src.School.Organization.Name, src.School.BranchName)))
                .ForMember(sm => sm.PrimaryTagDetail, s => s.MapFrom(src => src.TagDetail.Details))
                .ForMember(sm => sm.SecondaryTagDetail, s => s.MapFrom(src => src.TagDetail.Details));

            Mapper.AssertConfigurationIsValid(typeof(StudentProfile).Name);
        }
    }
}
