using AutoMapper;
using RoomLocator.Domain.Models;
using RoomLocator.Domain.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace RoomLocator.Domain.Config.AutoMapperConfigs
{
    /// <summary>
    ///     <author>Andreas Gøricke, s153804</author>
    /// </summary>
    public class MazeMapSectionSensorMapping : Profile
    {
        public MazeMapSectionSensorMapping()
        {
            CreateMap<MazeMapSectionSensor, MazeMapSectionSensorViewModel>().ReverseMap();
        }
    }
}
