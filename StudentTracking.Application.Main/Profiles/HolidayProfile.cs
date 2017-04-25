using AutoMapper;
using StudentTracking.Application.Models;

namespace StudentTracking.Application.Main.Profiles
{
    public class HolidayProfile : ProfileBase
    {
        protected override void Configure()
        {
            Mapper.CreateMap<Data.Holiday, HolidayModel>().ReverseMap();

            Mapper.CreateMap<Data.Holiday, EventModel>().ReverseMap()
                .ForMember(entity => entity.Id, map => map.Ignore())
                .ForMember(entity => entity.SchoolId, map => map.Ignore());

            Mapper.AssertConfigurationIsValid(typeof(HolidayProfile).Name);
        }
    }
}
