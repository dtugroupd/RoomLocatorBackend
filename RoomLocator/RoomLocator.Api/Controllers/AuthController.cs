using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using AutoMapper;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using RoomLocator.Data.Services;
using RoomLocator.Domain.InputModels;
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
        private readonly CampusNetAuthService _campusNetAuthService;
        private readonly TokenService _tokenService;
        private readonly ILogger<AuthController> _logger;
        private readonly IMapper _mapper;

        public AuthController(IHttpClientFactory clientFactory, IConfiguration config, UserService userService, CampusNetAuthService campusNetAuthService, TokenService tokenService, ILogger<AuthController> logger, IMapper mapper)
        {
            _clientFactory = clientFactory;
            _config = config;
            _userService = userService;
            _campusNetAuthService = campusNetAuthService;
            _tokenService = tokenService;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpPost("login")]
        public async Task<ActionResult<TokenViewModel>> CampusnetLogin(CnAuthInputModel authenticationModel)
        {
            var authenticatedUser = await _campusNetAuthService.Authenticate(authenticationModel);

            if (authenticatedUser == null) return Unauthorized(@"Incorrect DTU Credentials");

            var user = await _userService.GetOrCreate(authenticatedUser, authenticationModel.LoginModel.HasAcceptedDisclaimer);
            
            var token = new TokenViewModel
            {
                User = _mapper.Map<UserViewModel>(user),
                Token = await _tokenService.GenerateUserTokenAsync(user.StudentId)
            };

            return token;
        }

        [HttpGet("validate")]
        public async Task<IActionResult> ValidateSsoTicket(string ticket)
        {
            var service = new Uri(Request.GetDisplayUrl()).GetLeftPart(UriPartial.Path);
            if (service.Contains("://api"))
            {
                service = service.Replace("://api", "://se2-webapp04.compute.dtu.dk");
            }

            if (service.Contains("http://"))
            {
                service = service.Replace("http://", "https://");
            }

            _logger.LogInformation($"Validating DTU CAS Ticket. Service = '{service}', Ticket = '{ticket}'");
            var validateUrl = $"https://auth.dtu.dk/dtu/validate?service={service}&ticket={ticket}";
            _logger.LogInformation($"Validation URL: {validateUrl}");

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
            _logger.LogInformation($"Validating user '{studentId}'");
            var existingUser = await _userService.GetByStudentId(studentId) ?? await _userService.Create(studentId, true);
            _logger.LogInformation($"Found user, user id '{existingUser.Id}' with roles '{existingUser.Roles}'");

            var token = await _tokenService.GenerateUserTokenAsync(existingUser.StudentId);
            var tokenObject = new TokenViewModel
            {
                Token = token,
                User = existingUser
            };

            var contractResolver = new DefaultContractResolver { NamingStrategy = new CamelCaseNamingStrategy() };
            var json = JsonConvert.SerializeObject(tokenObject, new JsonSerializerSettings { ContractResolver = contractResolver });
            var base64 = Convert.ToBase64String(Encoding.UTF8.GetBytes(json));

            var uriBuilder = new UriBuilder($"{_config["frontendUrl"]}/validate");
            var query = HttpUtility.ParseQueryString(uriBuilder.Query);
            query["token"] = base64;

            uriBuilder.Query = query.ToString();

            return Redirect(uriBuilder.Uri.AbsoluteUri);
        }
        
        private IActionResult RedirectWithError(string error, int statusCode)
        {
            var errorObject = new
            {
                statusCode,
                error
            };

            var json = JsonConvert.SerializeObject(errorObject);
            var base64 = Convert.ToBase64String(Encoding.UTF8.GetBytes(json));

            var uriBuilder = new UriBuilder(
            
            _config["frontendUrl"]);
            var query = HttpUtility.ParseQueryString(uriBuilder.Query);

            query["error"] = base64;
            uriBuilder.Query = query.ToString();
            
            _logger.LogError($"Failed login with error: '{error}'");

            return Redirect(uriBuilder.Uri.AbsoluteUri);
        }
    }
}
