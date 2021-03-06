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
        private readonly FeedbackService _feedbackService;
        public FeedbackController(FeedbackService service)
        {
            _feedbackService = service;
        }

        [HttpGet(Name = nameof(GetFeedback))]
        public async Task<ActionResult<FeedbackViewModel>> GetFeedback(string id)
        {
            try
            {
                return Ok(await _feedbackService.Get(id));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("[action]")]
        public async Task<ActionResult<FeedbackViewModel>> GetCurrentUserFeedback(string userId, string locationId)
        {
            try
            {
                return Ok(await _feedbackService.GetCurrentUserFeedback(userId, locationId));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost("[action]")]
        public async Task<ActionResult<FeedbackViewModel>> Create(FeedbackInputModel feedback)
        {
            try
            {
                var createdFeedback = await _feedbackService.Create(feedback);
                return CreatedAtRoute(nameof(GetFeedback), new { id = createdFeedback.Id }, createdFeedback);
            }
            catch (InvalidRequestException e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut("[action]")]
        public async Task<ActionResult<FeedbackViewModel>> Update(FeedbackUpdateInputModel feedback)
        {
            try
            {
                var updatedFeedback = await _feedbackService.Update(feedback);
                return Ok(updatedFeedback);
            }
            catch (InvalidRequestException e)
            {
                return BadRequest(e.Message);
            }
        }
        }

    }

