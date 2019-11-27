using AutoMapper;
using RoomLocator.Domain.InputModels;
using RoomLocator.Domain.Models;
using RoomLocator.Domain.ViewModels;

namespace RoomLocator.Domain.Config.AutoMapperConfigs
{
    public class SensorMapping : Profile
    {
        public SensorMapping()
        {
            CreateMap<SensorViewModel, Sensor>().ReverseMap();
            CreateMap<SensorInputModel, Sensor>().ReverseMap();
        }
        
    }
}