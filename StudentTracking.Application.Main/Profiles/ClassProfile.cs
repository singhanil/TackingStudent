using AutoMapper;
using StudentTracking.Application.Models;
using StudentTracking.Data;

namespace StudentTracking.Application.Main.Profiles
{
    public class ClassProfile : ProfileBase
    {
        protected override void Configure()
        {
            Mapper.CreateMap<Class, ClassModel>().ReverseMap();

            Mapper.AssertConfigurationIsValid(typeof(ClassProfile).Name);
        }
    }
}
