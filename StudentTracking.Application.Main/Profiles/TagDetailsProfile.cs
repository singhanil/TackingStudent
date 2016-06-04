using AutoMapper;
using StudentTracking.Application.Models;
using StudentTracking.Data;

namespace StudentTracking.Application.Main.Profiles
{
    public class TagDetailsProfile : ProfileBase
    {
        protected override void Configure()
        {
            Mapper.CreateMap<TagDetail, TagDetailModel>().ReverseMap();

            Mapper.AssertConfigurationIsValid(typeof(TagDetailsProfile).Name);
        }
    }
}
