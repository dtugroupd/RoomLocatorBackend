using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RoomLocator.Data.Services;
using RoomLocator.Domain.InputModels;
using RoomLocator.Domain.ViewModels;
using Shared;

namespace RoomLocator.Api.Controllers
{
    /// <summary>
    ///     <author>Andreas Gøricke, s153804</author>
    /// </summary>
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
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
        
        [HttpPost("[action]")]
        public async Task<ActionResult<EventViewModel>> Create([FromBody] EventInputModel eventInput)
        {
            if (!ModelState.IsValid)
                return BadRequest();
            try
            {
                var createdEvent = await _service.CreateEvent(eventInput);
                return CreatedAtRoute(nameof(GetEvent), new { id = createdEvent.Id }, createdEvent);
            } catch(InvalidRequestException e) {
                return BadRequest(e.Message);
            }
        }
        
    }
}