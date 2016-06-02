using AutoMapper;
using StudentTracking.Application.Models;
using StudentTracking.Data;

namespace StudentTracking.Application.Main.Profiles
{
    public class OrganizationProfile : ProfileBase
    {
        protected override void Configure()
        {
            Mapper.CreateMap<Organization, OrganizationModel>();

            Mapper.CreateMap<OrganizationModel, Organization>()
                .ForMember(model => model.IsActive, map => map.Ignore())
                .ForMember(model => model.CreatedDate, map => map.Ignore())
                .ForMember(model => model.ModifiedDate, map => map.Ignore());

            Mapper.AssertConfigurationIsValid(typeof(OrganizationProfile).Name);
        }
    }
}
