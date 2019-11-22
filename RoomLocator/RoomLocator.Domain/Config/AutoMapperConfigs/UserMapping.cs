using AutoMapper;
using RoomLocator.Domain.InputModels;
using RoomLocator.Domain.ViewModels;

namespace RoomLocator.Domain.Config.AutoMapperConfigs
{
    public class UserMapping : Profile
    {
        public UserMapping()
        {
            CreateMap<User, UserViewModel>().ReverseMap();
            CreateMap<CnUserViewModel, UserViewModel>()
                .ForMember(dest => dest.FirstName,
                    opt => opt.MapFrom(from => from.GivenName))
                .ForMember(dest => dest.LastName,
                    opt => opt.MapFrom(from => from.FamilyName))
                .ForMember(dest => dest.StudentId,
                    opt => opt.MapFrom(from => from.UserName));
        }
    }
}
