using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RoomLocator.Domain.ViewModels;
using Shared;

namespace RoomLocator.Data.Services
{
    /// <summary>
    /// 	<author>Amal Qasim, s132957</author>
    /// </summary>
    public class ModcamService
    {
        private readonly HttpClient _httpClient;
        private readonly LocalCredentialsService _credentialsService;

        public ModcamService(HttpClient httpClient, LocalCredentialsService credentialsService)
        {
            _httpClient = httpClient;
            _credentialsService = credentialsService;
        }

        private async Task<HttpRequestMessage> MakeModcamHttpRequest(string url)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, url);

            var modcamCredentials = await _credentialsService.LoadModcamCredentials();
            
            request.Headers.Add("x-client-id", modcamCredentials.ClientId);
            request.Headers.Add("x-api-key", modcamCredentials.Key);

            return request;
        }

        public async Task<IEnumerable<ModcamInstallationsViewModel>> GetPeopleCounterInstallations()
        {
            var request = await MakeModcamHttpRequest("https://eds.modcam.io/v1/peoplecounter/installations");

            var response = await _httpClient.SendAsync(request);

            if (!response.IsSuccessStatusCode)
            {
                throw new InvalidRequestException("Failed to fetch people counter", response.ReasonPhrase);
            }
            
            return JsonConvert.DeserializeObject<ModcamInstallationsViewModel[]>(await response.Content.ReadAsStringAsync());
        }
    }
}
