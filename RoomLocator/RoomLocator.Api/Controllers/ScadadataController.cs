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
        private readonly ScadadataService _service;
        public ScadadataController(ScadadataService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ScadadataViewModel>>> GetSensorData()
        {
            return Ok(await _service.GetSensorData());
        }
        
        [HttpGet("[action]")]
        public async Task<ActionResult<IEnumerable<ScadadataScoresModel>>> GetListOfScores()
        {
            return Ok(await _service.GetListOfScores());
        }
        
        [HttpGet("[action]")]
        public async Task<ActionResult<double>> GetWeightedScore()
        {
            return Ok(await _service.GetWeightedScore());
        }
    }
}