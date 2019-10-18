using Microsoft.AspNetCore.Mvc;
using RoomLocator.Data.Services;
using RoomLocator.Domain.ViewModels;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RoomLocator.Api.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class SurveyController : ControllerBase
    {
        private readonly SurveyService _service;
        public SurveyController(SurveyService service)
        {
            _service = service;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SurveyViewModel>> Get(int id)
        {
            return Ok(await _service.Get(id));
        }

        [HttpPost("[action]")]
        public async Task<ActionResult<SurveyViewModel>> Create([FromBody] SurveyCreateViewModel survey)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            try
            {
                var createdSurvey = await _service.CreateSurvey(survey);
                return CreatedAtAction(nameof(Get), new { id = createdSurvey.Id });
            } catch(InvalidRequestException e) {
                return BadRequest(e.Message);
            }
        }
    }
}
