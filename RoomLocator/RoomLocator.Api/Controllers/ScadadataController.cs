using Microsoft.AspNetCore.Mvc;
using RoomLocator.Data.Services;
using RoomLocator.Domain.ViewModels;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Newtonsoft.Json;


namespace RoomLocator.Api.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class ScadadataController :Controller
    {
        private readonly IHttpClientFactory _clientFactory;

        public ScadadataController(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ScadadataViewModel>>> GetSensorData()
        {
            var url = "https://scadadataapi.azurewebsites.net/api/values";
            var request = new HttpRequestMessage(HttpMethod.Get, url);
            ScadadataService mc = new  ScadadataService();
            
            
            var client = _clientFactory.CreateClient("dtu-cas");
            var response = await client.SendAsync(request);
            
            if (!response.IsSuccessStatusCode)return Unauthorized();
            
            var scadadataVM = JsonConvert.DeserializeObject < List<ScadadataViewModel>>(await response.Content.ReadAsStringAsync());

            return scadadataVM;
        }
    }
}