using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Xml.Linq;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RoomLocator.Domain.InputModels;
using RoomLocator.Domain.ViewModels;
using Shared;

namespace RoomLocator.Data.Services
{
    /// <summary>
    /// Authenticating with old CampusNet API
    ///     <author>Anders Wiberg Olsen, s165241</author>
    /// </summary>
    public class CampusNetAuthService
    {
        private readonly ILogger<CampusNetAuthService> _logger;
        private readonly LocalCredentialsService _credentialsService;
        private readonly HttpClient _http;

        public CampusNetAuthService(IHttpClientFactory httpFactory, ILogger<CampusNetAuthService> logger, LocalCredentialsService credentialsService)
        {
            _logger = logger;
            _credentialsService = credentialsService;
            _http = httpFactory.CreateClient(nameof(CampusNetAuthService));
        }

        /// <summary>
        /// Sets token headers etc on the HttpRequest
        /// </summary>
        /// <param name="request">The HttpRequest to receive headers</param>
        /// <param name="username">CampusNet Username, i.e. Student Id</param>
        /// <param name="password">Limited password (token) can be null (in which case no auth header is set)</param>
        private async Task<HttpRequestMessage> SetHeaders(HttpRequestMessage request, string username, string password)
        {
            var crendentials = await _credentialsService.LoadCampusNetCredentials();
            var xname = crendentials.AppName;  // Todo: Read appname from file
            var xtoken = crendentials.ApiToken; // Todo: Read token from file

            _logger.LogTrace("Using CampusNet X-appname {X-appname} and X-token {X-token}", xname, xtoken);
            
            request.Headers.Add("Accept", "application/json");
            request.Headers.Add("X-appname", xname);
            request.Headers.Add("X-token", xtoken);   

            if (username == null || password == null) return request;

            _logger.LogInformation("Setting authorization header on request to {RequestUrl}", request.RequestUri);
            var authString = Convert.ToBase64String(System.Text.Encoding.ASCII.GetBytes($"{username}:{password}"));
            request.Headers.Authorization = 
                new AuthenticationHeaderValue("Basic", authString);

            return request;
        }

        /// <summary>
        /// Authenticates and fetches user details directly from CampusNet API
        /// </summary>
        /// <param name="authenticationModel">User's credentials for signing in</param>
        /// <returns>A user with details and image fetched from CampusNet API</returns>
        /// <exception cref="Exception"></exception>
        public async Task<CnUserViewModel> Authenticate(CnAuthInputModel authenticationModel)
        {
            var limitedPassword = await FetchLimitedPassword(authenticationModel);

            if (limitedPassword == null) throw ExceptionFactory.Unauthorized();
            
            var user = await FetchUserInformation(authenticationModel.LoginModel.Username, limitedPassword);
            user.ProfileImage = await GetProfilePicture(user.UserId, authenticationModel.LoginModel.Username, limitedPassword);
            return user;
        }

        /// <summary>
        /// Authenticates with CampusNet API and gets a limited password (token) for the user
        /// </summary>
        /// <param name="authenticationModel">Username and password</param>
        /// <returns>A limited password (token) on success, or null if the credentials are incorrect</returns>
        private async Task<string> FetchLimitedPassword(CnAuthInputModel authenticationModel)
        {
            const string authUrl = "https://auth.dtu.dk/dtu/mobilapp.jsp";

            var auth = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("Username", authenticationModel.LoginModel.Username),
                new KeyValuePair<string, string>("Password", authenticationModel.LoginModel.Password)
            };

            var authenticationRequest = new HttpRequestMessage(HttpMethod.Post, authUrl)
            {
                Content = new FormUrlEncodedContent(auth)
            };

            _logger.LogInformation("Trying to sign in using CampusNet as user {Username}", authenticationModel.LoginModel.Username);
            var authenticationResponse = await SendRequest(authenticationRequest);

            return
                ParseAuthenticationRequest(XElement.Parse(await authenticationResponse.Content.ReadAsStringAsync()));
        }

        /// <summary>
        /// Send a HTTP Request and handle errors
        /// </summary>
        /// <param name="request">The request to send</param>
        /// <returns>The response form the server</returns>
        /// <exception cref="InvalidRequestException">Thrown if the server is down or there is no access to the given URI</exception>
        private async Task<HttpResponseMessage> SendRequest(HttpRequestMessage request)
        {
            HttpResponseMessage response;

            try
            {
                response = await _http.SendAsync(request);
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError("Failed to send request to {RequestUrl}. Threw exception {Exception} with message {Message}", request.RequestUri, ex.GetType().Name, ex.Message);
                throw ExceptionFactory.Create_FailedHttpRequest("CampusNet");
            }

            return response;
        }

        /// <summary>
        /// Finds the limited password (token) from XML
        /// </summary>
        /// <param name="authResponse">XML Element</param>
        /// <returns>The limited password (token) if it exists, otherwise null</returns>
        private static string ParseAuthenticationRequest(XElement authResponse)
        {
            var success = authResponse.Elements("LimitedAccess").Any();
            return success ? authResponse.Element("LimitedAccess")?.Attribute("Password")?.Value : null;
        }

        /// <summary>
        /// Fetching user details including firstname, lastname, email etc.
        /// </summary>
        /// <param name="username">CampusNet Username, i.e. Student Id</param>
        /// <param name="limitedPassword">Limited password (token)</param>
        /// <returns>Detailed user view fetched from CampusNet API</returns>
        private async Task<CnUserViewModel> FetchUserInformation(string username, string limitedPassword)
        {
            var request = await PrepareRequest(HttpMethod.Get, "/CurrentUser/UserInfo", username, limitedPassword);
            _logger.LogInformation("Fetching user information for user {Username}", username);
            var response = await SendRequest(request);
            var content = await response.Content.ReadAsStringAsync();
            
            if (content.StartsWith("<"))
            {
                _logger.LogError("Something fucked up. Error: {Error}", content);
                var elm = XElement.Parse(content);
                if (elm.Elements("Reason").Any())
                {
                    var error = elm.Element("Reason").Element("Text").Value;
                    _logger.LogError("Didn't handle error: {Error}'", error);
                    throw new InvalidRequestException("Failed to fetch request", $"Failed with CampusNet error: {error}");
                }
            }
            
            var userInfo =
                JsonConvert.DeserializeObject<CnUserViewModel>(content);
            return userInfo;
        }

        /// <summary>
        /// Fetches the user's profile image and converts it to base64
        /// </summary>
        /// <param name="userId">The internal CampusNet user id, get be fetched from `FetchUserInformation`</param>
        /// <param name="username">CampusNet Username, i.e. Student Id</param>
        /// <param name="limitedPassword">Limited password (token)</param>
        /// <returns>Base64 encoded profile image from CampusNet</returns>
        private async Task<string> GetProfilePicture(string userId, string username, string limitedPassword)
        {
            var request = await PrepareRequest(HttpMethod.Get, $"/CurrentUser/Users/{userId}/Picture", username, limitedPassword);
            _logger.LogInformation("Fetching profile image for user {Username}", username);
            var response = await SendRequest(request);
            return Convert.ToBase64String(await response.Content.ReadAsByteArrayAsync());
        }

        /// <summary>
        /// Prepares a HttpRequest to the CampusNet API
        /// </summary>
        /// <param name="method">GET, POST etc</param>
        /// <param name="partialUrl">The endpoint to get to, i.e. /CurrentUser/UserInfo</param>
        /// <param name="username">CampusNet Username, i.e. Student Id</param>
        /// <param name="limitedPassword">Limited password (token)</param>
        /// <returns>A HttpRequest with correct authentication and headers, ready to be sent</returns>
        private async Task<HttpRequestMessage> PrepareRequest(HttpMethod method, string partialUrl, string username, string limitedPassword)
        {
            const string @base = "https://cn.inside.dtu.dk/data/";
            partialUrl = partialUrl.TrimStart('/');

            var request = new HttpRequestMessage(method, $"{@base}/{partialUrl}");
            request = await SetHeaders(request, username, limitedPassword);
            
            return request;
        }
    }
}
