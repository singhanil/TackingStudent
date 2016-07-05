using AutoMapper;
using StudentTracking.Application.Models.Models;
using StudentTracking.Data;

namespace StudentTracking.Application.Main.Profiles
{
    public class DepartmentProfile : ProfileBase
    {
        protected override void Configure()
        {
            Mapper.CreateMap<Department, DepartmentModel>().ReverseMap();

            Mapper.AssertConfigurationIsValid(typeof(DepartmentProfile).Name);
        }
    }
}
