using AutoMapper;
using RoomLocator.Domain.Models;
using RoomLocator.Domain.ViewModels;

namespace RoomLocator.Domain.Config.AutoMapperConfigs
{
    public class UserMapping : Profile
    {
        public UserMapping()
        {
            CreateMap<UserRole, RoleViewModel>()
                .ForMember(dest => dest.Name,
                    opt => opt.MapFrom(from => from.Role.Name))
                .ForMember(dest => dest.LocationId,
                    opt => opt.MapFrom(from => from.Location.Id))
                .ForMember(dest => dest.LocationName,
                    opt => opt.MapFrom(from => from.Location.Name));
            CreateMap<User, UserViewModel>()
                .ForMember(dest => dest.Roles,
                    opt => opt.MapFrom(from => from.UserRoles));
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
