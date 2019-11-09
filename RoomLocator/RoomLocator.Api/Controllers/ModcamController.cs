
using System.Collections.Generic;
using System.Net.Http;
using System.Runtime.Serialization.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RoomLocator.Data.Services;
using RoomLocator.Domain.ViewModels;

/// <summary>
/// 	<author>Amal Qasim, s132957</author>
/// </summary>

namespace RoomLocator.Api.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [Authorize]
    public class ModcamController :Controller
    {
        private readonly IHttpClientFactory _clientFactory;

        public ModcamController(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ModcamInstallationsViewModel>>> GetPeopleCounterInstallations()
        {
         
            var request = new ModcamService().RequestsForHttp();

            var client = _clientFactory.CreateClient("dtu-cas");
            var response = await client.SendAsync(request);
            
            if (!response.IsSuccessStatusCode)return Unauthorized();
            
            var modcamInstallations = JsonConvert.DeserializeObject < List<ModcamInstallationsViewModel>>(await response.Content.ReadAsStringAsync());
            return modcamInstallations;
        }
        
    }
}