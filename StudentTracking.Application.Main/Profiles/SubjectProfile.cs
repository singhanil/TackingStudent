using AutoMapper;
using StudentTracking.Application.Models;

namespace StudentTracking.Application.Main.Profiles
{
    public class SubjectProfile : ProfileBase
    {
        protected override void Configure()
        {
            Mapper.CreateMap<Data.Subject, SubjectModel>().ReverseMap();

            Mapper.AssertConfigurationIsValid(typeof(SubjectProfile).Name);
        }
    }
}
