using System.Linq;
using AutoMapper;
using RoomLocator.Domain.ViewModels;

namespace RoomLocator.Domain.Config.AutoMapperConfigs
{
    public class UserMapping : Profile
    {
        public UserMapping()
        {
            CreateMap<User, UserViewModel>()
                .ForMember(dest => dest.Roles,
                    opt => opt.MapFrom(from => from.UserRoles.Select(
                        ur => ur.Role.Name + 
                        (string.IsNullOrWhiteSpace(ur.Location.Name) ? "" : $"::{ur.Location.Name}"))
                        )
                    );
            CreateMap<CnUserViewModel, UserViewModel>()
                .ForMember(dest => dest.FirstName,
                    opt => opt.MapFrom(from => from.GivenName))
                .ForMember(dest => dest.LastName,
                    opt => opt.MapFrom(from => from.FamilyName))
                .ForMember(dest => dest.StudentId,
                    opt => opt.MapFrom(from => from.UserName));
            CreateMap<CnUserViewModel, User>()
                .ForMember(dest => dest.FirstName,
                    opt => opt.MapFrom(from => from.GivenName))
                .ForMember(dest => dest.LastName,
                    opt => opt.MapFrom(from => from.FamilyName))
                .ForMember(dest => dest.StudentId,
                    opt => opt.MapFrom(from => from.UserName));
            
        }
    }
}
