using Microsoft.AspNetCore.Mvc;
using RoomLocator.Data.Services;
using RoomLocator.Domain.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace RoomLocator.Api.Controllers
{
    /// <summary>
    ///     <author>Thomas Lien Christensen, s165242</author>
    /// </summary>
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [Authorize]
    public class LocationController : ControllerBase
    {
        private readonly LocationService _service;

        public LocationController(LocationService service)
        {
            _service = service;
        }
        
        [HttpGet]
        public async Task<ActionResult<IEnumerable<LocationSimpleViewModel>>> Get()
        {
            return Ok(await _service.GetLocations());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<LocationViewModel>> GetLocation(string id)
        {
            return Ok(await _service.GetLocation(id));
        }

    }
}