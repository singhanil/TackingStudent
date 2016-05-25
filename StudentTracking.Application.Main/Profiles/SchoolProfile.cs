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

            Mapper.CreateMap<SchoolModel, Data.School>()
                .ForMember(model => model.Id, map => map.Ignore())
                .ForMember(model => model.Organization, map => map.Ignore());

            Mapper.AssertConfigurationIsValid(typeof(SchoolProfile).Name);
        }
    }
}
