using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RoomLocator.Data.Services;
using RoomLocator.Domain.ViewModels;

namespace RoomLocator.Api.Controllers
{
    /// <summary>
    ///     <author> Amal Qasim, s132957 </author>
    /// </summary>
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class PeoplecounterController:Controller
    {
    
        private readonly IHttpClientFactory _clientFactory;

        public PeoplecounterController(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        [HttpGet]
        public async Task<ActionResult<PeoplecounterViewModel>> GetPeopleNumers()
        {
            var request = new PeoplecounterService().RequestsForHttp();

            var client = _clientFactory.CreateClient("dtu-cas");
            var response = await client.SendAsync(request);
            
            if (!response.IsSuccessStatusCode)return Unauthorized();
            
            var peopleCount = JsonConvert.DeserializeObject < PeoplecounterViewModel>(await response.Content.ReadAsStringAsync());
            return peopleCount;
        }
    }
}