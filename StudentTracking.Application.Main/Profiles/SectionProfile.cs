using AutoMapper;
using StudentTracking.Application.Models;
using StudentTracking.Data;

namespace StudentTracking.Application.Main.Profiles
{
    public class SectionProfile : ProfileBase
    {
        protected override void Configure()
        {
            Mapper.CreateMap<Section, SectionModel>().ReverseMap();

            Mapper.AssertConfigurationIsValid(typeof(SectionProfile).Name);
        }
    }
}
