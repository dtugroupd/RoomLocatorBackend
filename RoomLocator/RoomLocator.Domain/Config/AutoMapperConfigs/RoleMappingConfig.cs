using AutoMapper;
using RoomLocator.Domain.Models;
using RoomLocator.Domain.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace RoomLocator.Domain.Config.AutoMapperConfigs
{
    public class RoleMapping : Profile
    {
        public RoleMapping()
        {
            CreateMap<Role, RoleViewModel>().ReverseMap();
        }
    }
}
