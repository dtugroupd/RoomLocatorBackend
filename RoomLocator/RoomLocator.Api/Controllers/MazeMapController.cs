﻿using Microsoft.AspNetCore.Mvc;
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
    //[Authorize]
    public class MazeMapController : ControllerBase
    {
        private readonly MazeMapService _service;

        public MazeMapController(MazeMapService service)
        {
            _service = service;
        }

        [HttpGet]
        public ActionResult<MazeMapCoordinatesViewModel> GetStaticPoint()
        {
            return Ok(new MazeMapCoordinatesViewModel { Latitude = 55.78498471097425, Longitude = 12.52026114549633 });
        }

        
        [HttpGet("[action]")]
        public async Task<ActionResult<IEnumerable<MazeMapSectionViewModel>>> LibrarySections()
        {
            return Ok(await _service.GetSections());
        }
        
        [HttpGet("[action]")]
        public async Task<ActionResult<IEnumerable<MazeMapSectionSensorViewModel>>> LibrarySectionsWithSensors()
        {
            return Ok(await _service.GetSectionsWithSensors());
        }

    }
}