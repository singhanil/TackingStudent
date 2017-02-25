using AutoMapper;
using StudentTracking.Application.Models;
using StudentTracking.Data;

namespace StudentTracking.Application.Main.Profiles
{
    public class NotificationProfile : ProfileBase
    {
        protected override void Configure()
        {
            Mapper.CreateMap<Data.Notification, Models.NotificationNew>()
                .ForMember(model => model.MessageId, map => map.Ignore())
                .ForMember(model => model.SchoolId, map => map.Ignore());

            Mapper.AssertConfigurationIsValid(typeof(NotificationProfile).Name);
        }
    }
}
