using AutoMapper;
using RoomLocator.Domain.InputModels;
using RoomLocator.Domain.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

/// <summary>
/// 	<author>Amal Qasim, s132957, Gaurav Dangs:134692</author>
/// </summary>
namespace RoomLocator.Domain.Config.AutoMapperConfigs
{
    public class SensorMapping : Profile
    {
        public SensorMapping()
        {
            CreateMap<Sensor, SensorViewModel>().ReverseMap();
            CreateMap<Sensor, SensorInputModel>().ReverseMap();
        }
    }
}
