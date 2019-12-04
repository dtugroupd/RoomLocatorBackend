using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RoomLocator.Data.Services;
using RoomLocator.Domain.InputModels;
using RoomLocator.Domain.ViewModels;

namespace RoomLocator.Api.Controllers
{
    /// <summary>
    ///     <author>Andreas Gøricke, s153804</author>
    ///     <author>Anders Wiberg Olsen, s165241</author>
    /// </summary>
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [Authorize]
    [ApiController]

    public class EventController : ControllerBase
    {
        private readonly EventService _service;
        
        public EventController(EventService service)
        {
            _service = service;
        }
        
        [HttpGet("{id}", Name = nameof(GetEvent))]
        public async Task<ActionResult<EventViewModel>> GetEvent(string id)
        {
            return Ok(await _service.Get(id));
        }
        
        [HttpGet("[action]")]
        public async Task<ActionResult<IEnumerable<EventViewModel>>> GetAll()
        {
            return Ok(await _service.GetAll());
        }
        
        //[Authorize(Roles = "admin")]
        [HttpPost("[action]")]
        public async Task<ActionResult<EventViewModel>> Create([FromBody] EventInputModel eventInput)
        {
            var createdEvent = await _service.CreateEvent(eventInput);
            return CreatedAtRoute(nameof(GetEvent), new { id = createdEvent.Id }, createdEvent);
        }

        //[Authorize(Roles = "admin")]
        [HttpPut("[action]")]
        public async Task<ActionResult<EventViewModel>> Update([FromBody] EventUpdateInputModel eventInput)
        {
            return Ok(await _service.UpdateEvent(eventInput));
        }
    }
}
