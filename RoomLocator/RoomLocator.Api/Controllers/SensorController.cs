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

        //[httpput("{id}")]
        //public async task<actionresult<sensorviewmodel>> put(string id, [frombody] sensorinputmodel input)
        //{
        //    var updatedsensor;
        //        //update(id, value.name, value.type, value.longitude, value.latitude, 
        //    //value.timestamp, value.unit, value.value, value.status);
        //    return ok(updatedsensor);
        //}

        //[httpdelete("{id}")]
        //public async task<actionresult> delete(string id)
        //{
        //    await _sensorservice.delete(id);
        //    return nocontent();
        //}

    }
}
