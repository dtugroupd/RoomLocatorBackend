using System;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Logging;
using Moq;
using RoomLocator.Data.Config;
using RoomLocator.Domain.Config;

namespace RoomLocator.UnitTest
{
    public class BaseTest
    {
        protected readonly DbContextOptions<RoomLocatorContext> Options;
        protected readonly IMapper Mapper;
        protected ILogger<T> Logger<T>() => new Mock<ILogger<T>>().Object;

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
