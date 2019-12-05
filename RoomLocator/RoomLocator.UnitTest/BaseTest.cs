using System;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using RoomLocator.Data.Config;
using RoomLocator.Domain.Config;

namespace RoomLocator.UnitTest
{
    public class BaseTest
    {
        protected readonly DbContextOptions<RoomLocatorContext> Options;
        protected readonly IMapper Mapper;

        public BaseTest()
        {
            Options = new DbContextOptionsBuilder<RoomLocatorContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .ConfigureWarnings(x => x.Ignore(InMemoryEventId.TransactionIgnoredWarning))
                .Options;
            Mapper = AutoMapperConfig.CreateMapper();
        }
    }
}
