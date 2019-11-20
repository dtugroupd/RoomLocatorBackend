using System.Linq;
using AutoMapper;
using RoomLocator.Domain.InputModels;
using RoomLocator.Domain.ViewModels;

namespace RoomLocator.Domain.Config.AutoMapperConfigs
{
    public class UserMapping : Profile
    {
        public UserMapping()
        {
            CreateMap<User, UserViewModel>()
                .ForMember(
                    dest => dest.Roles, 
                    opt => opt.MapFrom(
                        from => from.UserRoles.Select(ur => ur.Role.Name)
                        )
                    );
        }
    }
}