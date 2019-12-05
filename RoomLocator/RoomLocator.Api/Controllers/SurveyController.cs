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
    public class SurveyController : ControllerBase
    {
        private readonly SurveyService _service;
        public SurveyController(SurveyService service)
        {
            _service = service;
        }

        [HttpGet("{id}", Name = nameof(Get))]
        public async Task<ActionResult<SurveyViewModel>> Get(string id)
        {
            return Ok(await _service.Get(id));
        }

        [HttpGet("SurveyAnswer/{id}", Name = nameof(GetSurveyAnswer))]
        public async Task<ActionResult<SurveyAnswerViewModel>> GetSurveyAnswer(string id)
        {
            return Ok(await _service.GetSurveyAnswer(id));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<SurveyViewModel>>> GetAll()
        {
            return Ok(await _service.GetAll());
        }

        [HttpPost("[action]")]
        [Authorize(Roles = "researcher,library")]
        public async Task<ActionResult<SurveyViewModel>> Create([FromBody] SurveyInputModel survey)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            try
            {
                var createdSurvey = await _service.CreateSurvey(survey);
                return CreatedAtRoute(nameof(Get), new { id = createdSurvey.Id }, createdSurvey);
            } catch(InvalidRequestException e) {
                return BadRequest(e.Message);
            }
        }

        [HttpPost("[action]")]
        public async Task<ActionResult<SurveyAnswerViewModel>> SubmitAnswer([FromBody] SurveyAnswerInputModel survey)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            try
            {
                var createdSurveyAnswer = await _service.SubmitAnswer(survey);
                return CreatedAtRoute(nameof(GetSurveyAnswer), new { id = createdSurveyAnswer.Id }, createdSurveyAnswer);
            }
            catch (InvalidRequestException e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("[action]/{id}")]
        public async Task<ActionResult> DownloadSurveyAnswers(string id)
        {
            try
            {
                var stream = await _service.GetSurveyAnswersCsvMemoryStream(id);
                return File(stream, "text/csv", $"Survey_{id}_answers_{DateTime.Now.ToShortDateString()}.csv");
            } catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
