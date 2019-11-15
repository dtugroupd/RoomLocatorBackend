using Microsoft.AspNetCore.Mvc;
using RoomLocator.Data.Services;
using RoomLocator.Domain.InputModels;
using RoomLocator.Domain.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Shared;

namespace RoomLocator.Api.Controllers
{
    /// <summary>
    /// 	<author>Amal Qasim, s132957</author>
    /// </summary>
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [Authorize (Roles = "admin")]
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
            if (!ModelState.IsValid) return BadRequest();
            
            try
            {
                 var createdSensor= await _sensorService.Create(input);
                 return createdSensor;
            }
            catch (InvalidRequestException e)
            {
                return BadRequest(e.Message);
            }
           
            
            
        }

        [HttpPut("id")]
        public async Task<ActionResult<SensorViewModel>> Put(string id, [FromBody]SensorInputModel sensor)
        {
            if (!ModelState.IsValid) return BadRequest();

            try
            {
                 var updateSensor = await _sensorService.Update(id, sensor); 
                 return Ok(updateSensor);
            }
            catch (InvalidRequestException e)
            {
                return BadRequest(e.Message);
            }
               
        }

        [HttpDelete("id")]
        public async Task<ActionResult> Delete(string id)
        {
            await _sensorService.Delete(id);
            return NoContent();
        }
         
    }
}
