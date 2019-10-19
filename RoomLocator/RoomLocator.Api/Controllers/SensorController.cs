using Microsoft.AspNetCore.Mvc;
using RoomLocator.Data.Services;
using RoomLocator.Domain.InputModels;
using RoomLocator.Domain.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RoomLocator.Api.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class SensorController : ControllerBase
    {
        private readonly SensorService _sensorService;

        public SensorController(SensorService sensorService)
        {
            _sensorService = sensorService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<SensorViewModel>>> Get()
        {
            var sensors = await _sensorService.Get();
            return Ok (sensors);
        }


        [HttpPost]
        public async Task<ActionResult<SensorViewModel>> Create(SensorInputModel input)
        {
            var createdSensor = await _sensorService.Create(input);
            return createdSensor;
        }

        [HttpPut("id")]
        public async Task<ActionResult<SensorViewModel>> Put(string id, [FromBody]SensorInputModel sensor)
        {
            var updateSenser = await _sensorService.Update(id, sensor);
            return Ok(updateSenser);
        }

        [HttpDelete("id")]
        public async Task<ActionResult> Delete(string id)
        {
            await _sensorService.Delete(id);
            return NoContent();
        }
         
    }
}
