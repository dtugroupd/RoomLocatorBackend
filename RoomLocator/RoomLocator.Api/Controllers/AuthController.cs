using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using RoomLocator.Data.Services;
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
        private readonly UserService _userService;
        private readonly TokenService _tokenService;

        public AuthController(IHttpClientFactory clientFactory, IConfiguration config, UserService userService, TokenService tokenService)
        {
            _clientFactory = clientFactory;
            _config = config;
            _userService = userService;
            _tokenService = tokenService;
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

            var studentId = responseMessage.Split("\n")[1];
            var existingUser = await _userService.GetByStudentId(studentId) ?? await _userService.Create(studentId);

            var token = await _tokenService.GenerateUserTokenAsync(existingUser.StudentId);

            return Redirect($"{_config["frontendUrl"]}/validate?token={token}");
        }
    }
}