using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RoomLocator.Domain.InputModels;
using RoomLocator.Domain.ViewModels;

namespace RoomLocator.Api.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IHttpClientFactory _clientFactory;

        public AuthController(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        [HttpGet("validate")]
        public async Task<ActionResult<TokenViewModel>> ValidateSsoTicket(string ticket)
        {
            var service = "https://localhost:5001/api/v1/auth/validate";
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

            return Redirect($"http://localhost:4200/validate?userId={responseMessage.Split("\n")[1]}");
        }
        
    }
}