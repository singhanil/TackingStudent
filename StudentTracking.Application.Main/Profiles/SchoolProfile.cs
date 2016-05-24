using AutoMapper;
using StudentTracking.Application.Models;

namespace StudentTracking.Application.Main.Profiles
{
    public class SchoolProfile : ProfileBase
    {
        protected override void Configure()
        {
            Mapper.CreateMap<Data.School, SchoolModel>()
                .ForMember(sm => sm.OrganizationName, s => s.MapFrom(src => src.Organization.Name));

            Mapper.AssertConfigurationIsValid(typeof(SchoolProfile).Name);
        }
    }
}
