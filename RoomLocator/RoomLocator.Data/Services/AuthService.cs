using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Xml.Linq;
using Newtonsoft.Json;
using RoomLocator.Domain.InputModels;
using RoomLocator.Domain.ViewModels;

namespace RoomLocator.Data.Services
{
    public class AuthService
    {
        private readonly HttpClient _http;

        public AuthService(IHttpClientFactory httpFactory)
        {
            _http = httpFactory.CreateClient(nameof(AuthService));
        }

        private void SetHeaders(ref HttpRequestMessage request, string username, string password)
        {
            request.Headers.Add("Accept", "application/json");
            request.Headers.Add("X-appname", ""); // Todo: Read appname from file
            request.Headers.Add("X-token", "");   // Todo: Read token from file

            if (username == null || password == null) return;

            var authString = Convert.ToBase64String(System.Text.Encoding.ASCII.GetBytes($"{username}:{password}"));
            request.Headers.Authorization = 
                new AuthenticationHeaderValue("Basic", authString);
        }

        public async Task<CnUserViewModel> Authenticate(CnAuthInputModel authenticationModel)
        {
            var limitedPassword = await FetchLimitedPassword(authenticationModel);

            if (limitedPassword == null) throw new Exception(); // Todo: Throw a proper unauthorized exception
            
            var user = await FetchUserInformation(authenticationModel.Username, limitedPassword);
            user.ProfileImage = await GetProfilePicture(user.UserId, authenticationModel.Username, limitedPassword);
            return user;
        }

        private async Task<string> FetchLimitedPassword(CnAuthInputModel authenticationModel)
        {
            var authUrl = "https://auth.dtu.dk/dtu/mobilapp.jsp";

            var auth = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("Username", authenticationModel.Username),
                new KeyValuePair<string, string>("Password", authenticationModel.Password)
            };
            
            var authenticationRequest = new HttpRequestMessage(HttpMethod.Post, authUrl);
            authenticationRequest.Content = new FormUrlEncodedContent(auth);

            var authenticationResponse = await _http.SendAsync(authenticationRequest);
            return
                ParseAuthenticationRequest(XElement.Parse(await authenticationResponse.Content.ReadAsStringAsync()));
        }

        private string ParseAuthenticationRequest(XElement authResponse)
        {
            var success = authResponse.Elements("LimitedAccess").Any();
            return success ? authResponse.Element("LimitedAccess")?.Attribute("Password")?.Value : null;
        }

        private async Task<CnUserViewModel> FetchUserInformation(string username, string limitedPassword)
        {
            var request = PrepareRequest(HttpMethod.Get, "/CurrentUser/UserInfo", username, limitedPassword);
            var response = await _http.SendAsync(request);
            var userInfo =
                JsonConvert.DeserializeObject<CnUserViewModel>(await response.Content.ReadAsStringAsync());
            return userInfo;
        }

        private async Task<string> GetProfilePicture(string userId, string username, string limitedPassword)
        {
            var request = PrepareRequest(HttpMethod.Get, $"/CurrentUser/Users/{userId}/Picture", username, limitedPassword);
            var response = await _http.SendAsync(request);
            return Convert.ToBase64String(await response.Content.ReadAsByteArrayAsync());
        }

        private HttpRequestMessage PrepareRequest(HttpMethod method, string partialUrl, string username, string limitedPassword)
        {
            var @base = "https://cn.inside.dtu.dk/data/";
            partialUrl = partialUrl.TrimStart('/');

            var request = new HttpRequestMessage(method, $"{@base}/{partialUrl}");
            SetHeaders(ref request, username, limitedPassword);
            
            return request;
        }
    }
}
