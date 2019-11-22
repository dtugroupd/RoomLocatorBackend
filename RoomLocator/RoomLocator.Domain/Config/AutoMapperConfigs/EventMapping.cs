using AutoMapper;
using RoomLocator.Domain.InputModels;
using RoomLocator.Domain.Models;
using RoomLocator.Domain.ViewModels;

namespace RoomLocator.Domain.Config.AutoMapperConfigs
{
    public class EventMapping : Profile
    {
        public EventMapping()
        {
            CreateMap<EventViewModel, Event>().ReverseMap();
            CreateMap<EventInputModel, Event>().ReverseMap();
        }
    }
}