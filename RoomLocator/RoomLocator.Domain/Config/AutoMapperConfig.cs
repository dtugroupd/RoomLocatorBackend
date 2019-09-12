using System;
using System.Linq;
using AutoMapper;
using RoomLocator.Domain.Config.AutoMapperConfigs;

namespace RoomLocator.Domain.Config
{
    public static class AutoMapperConfig
    {
        public static IMapper CreateMapper() => new MapperConfiguration(cfg =>
        {
            var profiles = typeof(ValueMapping).Assembly.GetTypes().Where(x => typeof(Profile).IsAssignableFrom(x));
            foreach (var profile in profiles)
            {
                cfg.AddProfile(Activator.CreateInstance(profile) as Profile);
            }
        }).CreateMapper();
    }
}
