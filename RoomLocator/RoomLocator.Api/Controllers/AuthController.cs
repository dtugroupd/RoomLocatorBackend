using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using RoomLocator.Data.Services;
using RoomLocator.Domain.ViewModels;

namespace RoomLocator.Api.Controllers
{
    /// <summary>
    /// <author>Anders Wiberg Olsen, s165241</author>
    /// </summary>
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
        public async Task<IActionResult> ValidateSsoTicket(string ticket)
        {
            var service = new Uri(Request.GetDisplayUrl()).GetLeftPart(UriPartial.Path);
            var validateUrl = $"https://auth.dtu.dk/dtu/validate?service={service}&ticket={ticket}";

            var request = new HttpRequestMessage(HttpMethod.Get, validateUrl);
            var client = _clientFactory.CreateClient("dtu-cas");
            var response = await client.SendAsync(request);

            if (!response.IsSuccessStatusCode)
            {
                return RedirectWithError("Failed to validate DTU Ticket", 500);
            }

            var responseMessage = await response.Content.ReadAsStringAsync();

            if (responseMessage.Trim().ToLower() == "no")
            {
                return RedirectWithError("You are not authorized to sign in", 401);
            }

            var studentId = responseMessage.Split("\n")[1];
            var existingUser = await _userService.GetByStudentId(studentId) ?? await _userService.Create(studentId);

            var token = await _tokenService.GenerateUserTokenAsync(existingUser.StudentId);
            var tokenObject = new TokenViewModel
            {
                Token = token,
                User = existingUser
            };

            var json = JsonConvert.SerializeObject(tokenObject);
            var base64 = Convert.ToBase64String(Encoding.UTF8.GetBytes(json));

            var uriBuilder = new UriBuilder($"{_config["frontendUrl"]}/validate");
            var query = HttpUtility.ParseQueryString(uriBuilder.Query);
            query["token"] = base64;

            uriBuilder.Query = query.ToString();

            return Redirect(uriBuilder.Uri.AbsoluteUri);
        }
        
        private IActionResult RedirectWithError(string error, int statusCode) {
            var errorObject = new {
                statusCode,
                error
            };

            var json = JsonConvert.SerializeObject(errorObject);
            var base64 = Convert.ToBase64String(Encoding.UTF8.GetBytes(json));

            var uriBuilder = new UriBuilder(_config["frontendUrl"]);
            var query = HttpUtility.ParseQueryString(uriBuilder.Query);

            query["error"] = base64;
            uriBuilder.Query = query.ToString();

            return Redirect(uriBuilder.Uri.AbsoluteUri);
        }
    }
}
