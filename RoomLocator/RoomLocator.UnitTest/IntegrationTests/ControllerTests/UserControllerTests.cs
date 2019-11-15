using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace RoomLocator.UnitTest.IntegrationTests.ControllerTests
{
    public class UserControllerTests : IClassFixture<RoomLocatorApplicationFactory<Api.Startup>>
    {
        private readonly HttpClient _client;
        private readonly RoomLocatorApplicationFactory<Api.Startup> _factory;

        public UserControllerTests(RoomLocatorApplicationFactory<Api.Startup> factory)
        {
            _factory = factory;
            _client = _factory.CreateClient(new WebApplicationFactoryClientOptions
            {
                AllowAutoRedirect = false
            });
        }

        [Fact]
        public async Task GetUserMe()
        {
            var page = await _client.GetAsync("/me");
            var content = await page.Content.ReadAsStringAsync();
            
            Console.WriteLine("stuff");
        }
    }
}