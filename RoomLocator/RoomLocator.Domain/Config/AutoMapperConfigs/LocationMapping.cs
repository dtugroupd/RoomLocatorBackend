using AutoMapper;
using RoomLocator.Domain.Models;
using RoomLocator.Domain.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RoomLocator.Domain.Config.AutoMapperConfigs
{
    /// <summary>
    ///     <author>Thomas Lien Christensen, s165242</author>
    /// </summary>
    public class LocationMappingProfile : Profile
    {
        public LocationMappingProfile()
        {
            CreateMap<Location, LocationViewModel>()
                .ForMember(
                    x => x.Coordinates,
                    opt => opt.MapFrom(src => src.Coordinates.OrderBy(x => x.Index)));

            CreateMap<Location, LocationSimpleViewModel>()
                .ForMember(
                    x => x.Coordinates,
                    opt => opt.MapFrom(src => src.Coordinates.OrderBy(x => x.Index)));
        }
    }
}
