using AutoMapper;
using RoomLocator.Domain.InputModels;
using RoomLocator.Domain.Models;
using RoomLocator.Domain.ViewModels;

namespace RoomLocator.Domain.Config.AutoMapperConfigs
{
    /// <summary>
    ///     <author>Thomas Lien Christensen, s165242</author>
    /// </summary>

    public class EventMapping : Profile
    {
        public EventMapping()
        {
            CreateMap<Event, EventViewModel>()
                .ForMember(dest => dest.LocationName,
                opt => opt.MapFrom(src => src.Location.Name));

            CreateMap<EventViewModel, Event>();
            CreateMap<EventInputModel, Event>().ReverseMap();
            CreateMap<EventUpdateInputModel, Event>().ReverseMap();
        }
    }
}