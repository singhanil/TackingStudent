using AutoMapper;
using StudentTracking.Application.Models;
using StudentTracking.Data;

namespace StudentTracking.Application.Main.Profiles
{
    public class CountryProfile : ProfileBase
    {
        protected override void Configure()
        {
            Mapper.CreateMap<Country, CountryModel>().ReverseMap();

            Mapper.AssertConfigurationIsValid(typeof(CountryProfile).Name);
        }
    }
}
