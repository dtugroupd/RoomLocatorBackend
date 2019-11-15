using AutoMapper;
using RoomLocator.Domain.InputModels;
using RoomLocator.Domain.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

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
