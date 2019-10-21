﻿using Microsoft.AspNetCore.Mvc;
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

        [HttpGet]
        public async Task<ActionResult<IEnumerable<SurveyViewModel>>> GetAll()
        {
            return Ok(await _service.GetAll());
        }

        [HttpPost("[action]")]
        public async Task<ActionResult<SurveyViewModel>> Create([FromBody] SurveyCreateViewModel survey)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            try
            {
                var createdSurvey = await _service.CreateSurvey(survey);
                return NoContent();

                // Doesn't work right now. Why?
                //return CreatedAtAction(nameof(Get), new { id = createdSurvey.Id }, createdSurvey);
            } catch(InvalidRequestException e) {
                return BadRequest(e.Message);
            }
        }

        [HttpPost("[action]")]
        public async Task<ActionResult<SurveyAnswerViewModel>> SubmitAnswer([FromBody] SurveyAnswerSubmitViewModel survey)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            try
            {
                await _service.SubmitAnswer(survey);
                return NoContent();
            }
            catch (InvalidRequestException e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
