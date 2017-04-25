using AutoMapper;
using StudentTracking.Application.Models;
using StudentTracking.Data;

namespace StudentTracking.Application.Main.Profiles
{
    public class ImportantLinkProfile : ProfileBase
    {
        protected override void Configure()
        {
            Mapper.CreateMap<Important_Links, ImpotantLinkModel>().ReverseMap();

            Mapper.AssertConfigurationIsValid(typeof(ImportantLinkProfile).Name);
        }
    }
}
