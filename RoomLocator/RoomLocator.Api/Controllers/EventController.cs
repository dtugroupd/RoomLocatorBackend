using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RoomLocator.Data.Services;
using RoomLocator.Domain.InputModels;
using RoomLocator.Domain.ViewModels;
using Shared.Extentions;

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
        private readonly UserService _userService;
        
        public EventController(EventService service, UserService userService)
        {
            _service = service;
            _userService = userService;
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

        [Authorize(Roles = "admin")]
        [HttpPost("[action]")]
        public async Task<ActionResult<EventViewModel>> Create([FromBody] EventInputModel eventInput)
        {
            await _userService.EnsureAdmin(User.StudentId(), eventInput.LocationId);

            var createdEvent = await _service.CreateEvent(eventInput);
            return CreatedAtRoute(nameof(GetEvent), new { id = createdEvent.Id }, createdEvent);
        }

        [Authorize(Roles = "admin")]
        [HttpPut("[action]")]
        public async Task<ActionResult<EventViewModel>> Update([FromBody] EventUpdateInputModel eventInput)
        {
            await _userService.EnsureAdmin(User.StudentId(), eventInput.LocationId);

            return Ok(await _service.UpdateEvent(eventInput));
        }
    }
}
