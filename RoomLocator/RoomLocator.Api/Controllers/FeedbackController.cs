using Microsoft.AspNetCore.Mvc;
using RoomLocator.Data.Services;
using RoomLocator.Domain.InputModels;
using RoomLocator.Domain.Models;
using RoomLocator.Domain.ViewModels;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RoomLocator.Api.Controllers
{
    /// <summary>
    ///     <author>Hamed kadkhodaie, s083485</author>
    /// </summary>
    
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]

    public class FeedbackController : ControllerBase
    {
        private readonly FeedbackService _service;
        public FeedbackController(FeedbackService service)
        {
            _service = service;
        }


        [HttpPost("[action]")]
        public async Task<ActionResult<FeedbackViewModel>> Create([FromBody] FeedbackInputModel feedback)
        {
            if (!ModelState.IsValid)
                return BadRequest();

        }

    }

}