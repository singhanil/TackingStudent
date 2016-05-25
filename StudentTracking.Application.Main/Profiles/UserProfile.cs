using AutoMapper;
using StudentTracking.Application.Models;
using StudentTracking.Data;

namespace StudentTracking.Application.Main.Profiles
{
    public class UserProfile : ProfileBase
    {
        protected override void Configure()
        {
            Mapper.CreateMap<User, UserModel>().ReverseMap();

            Mapper.AssertConfigurationIsValid(typeof(UserProfile).Name);
        }
    }
}
