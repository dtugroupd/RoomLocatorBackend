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
    /// <summary>
    ///    <author>Andreas Gøricke, s153804</author>
    /// </summary>
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [Authorize]
    public class ScadadataController :Controller
    {
        private readonly ScadadataService _scadadataservice;
        public ScadadataController(ScadadataService service)
        {
            _scadadataservice = service;
        }

        [HttpGet("[action]")]
        public async Task<ActionResult<IEnumerable<ScadadataScoresModel>>> GetStatus()
        {
            return Ok(await _scadadataservice.GetListOfScores());
        }
        
     
    }
}