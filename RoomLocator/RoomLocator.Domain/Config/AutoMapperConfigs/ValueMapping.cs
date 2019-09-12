using AutoMapper;
using RoomLocator.Domain.InputModels;
using RoomLocator.Domain.ViewModels;

namespace RoomLocator.Domain.Config.AutoMapperConfigs
{
    public class ValueMapping : Profile
    {
        public ValueMapping()
        {
            CreateMap<ValueViewModel, Value>().ReverseMap();
            CreateMap<ValueInputModel, Value>().ReverseMap();
        }
    }
}
