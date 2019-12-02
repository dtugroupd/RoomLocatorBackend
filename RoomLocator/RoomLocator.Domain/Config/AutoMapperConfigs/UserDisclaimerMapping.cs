using AutoMapper;
using RoomLocator.Domain.Models;
using RoomLocator.Domain.ViewModels;

namespace RoomLocator.Domain.Config.AutoMapperConfigs
{
    public class UserDisclaimerMapping : Profile
    {
        public UserDisclaimerMapping()
        {
            CreateMap<UserDisclaimer, UserDisclaimerViewModel>();
        }
    }
}
