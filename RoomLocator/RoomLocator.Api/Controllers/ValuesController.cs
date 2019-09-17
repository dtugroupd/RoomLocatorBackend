using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RoomLocator.Data;
using RoomLocator.Data.Services;
using RoomLocator.Domain;
using RoomLocator.Domain.InputModels;
using RoomLocator.Domain.ViewModels;

namespace RoomLocator.Api.Controllers
{
    /// <summary>
    /// Demo Controller - can be used as inspiration
    /// </summary>
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly ValueService _valueService;

        public ValuesController(ValueService valueService)
        {
            _valueService = valueService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ValueViewModel>>> Get()
        {
            return Ok(await _valueService.Get());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ValueViewModel>> Get(string id)
        {
            return Ok(await _valueService.Get(id));
        }

        [HttpPost]
        public async Task<ActionResult<ValueViewModel>> Post([FromBody] ValueInputModel value)
        {
            var createdValue = await _valueService.Create(value);
            return CreatedAtAction(nameof(Get), new {id = createdValue.Id}, createdValue);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ValueViewModel>> Put(string id, [FromBody] ValueInputModel value)
        {
            var updatedValue = await _valueService.Update(id, value);
            return Ok(updatedValue);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(string id)
        {
            await _valueService.Delete(id);
            return NoContent();
        }
    }
}
