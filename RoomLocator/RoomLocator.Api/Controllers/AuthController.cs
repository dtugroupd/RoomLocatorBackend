using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using RoomLocator.Domain.ViewModels;

namespace RoomLocator.Api.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly IConfiguration _config;

        public AuthController(IHttpClientFactory clientFactory, IConfiguration config)
        {
            _clientFactory = clientFactory;
            _config = config;
        }

        [HttpGet("validate")]
        public async Task<ActionResult<TokenViewModel>> ValidateSsoTicket(string ticket)
        {
            var service = new Uri(Request.GetDisplayUrl()).GetLeftPart(UriPartial.Path);
            var validateUrl = $"https://auth.dtu.dk/dtu/validate?service={service}&ticket={ticket}";

            var request = new HttpRequestMessage(HttpMethod.Get, validateUrl);
            var client = _clientFactory.CreateClient("dtu-cas");
            var response = await client.SendAsync(request);

            if (!response.IsSuccessStatusCode)
            {
                return Unauthorized();
            }

            var responseMessage = await response.Content.ReadAsStringAsync();

            if (responseMessage.Trim().ToLower() == "no")
            {
                return Unauthorized();
            }

            return Redirect($"{_config["frontendUrl"]}/validate?userId={responseMessage.Split("\n")[1]}");
        }
    }
}