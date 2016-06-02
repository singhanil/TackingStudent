using AutoMapper;

using StudentTracking.Application.Models;
using StudentTracking.Data;

namespace StudentTracking.Application.Main.Profiles
{
    public class StateProfile : ProfileBase
    {
        protected override void Configure()
        {
            Mapper.CreateMap<State, StateModel>();

            Mapper.CreateMap<StateModel, State>()
                .ForMember(model => model.IsActive, map => map.Ignore())
                .ForMember(model => model.Country, map => map.Ignore());

            Mapper.AssertConfigurationIsValid(typeof(StateProfile).Name);
        }
    }
}
