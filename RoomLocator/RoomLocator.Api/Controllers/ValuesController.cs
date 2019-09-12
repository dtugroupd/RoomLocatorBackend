using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RoomLocator.Data;
using RoomLocator.Domain;
using RoomLocator.Domain.InputModels;
using RoomLocator.Domain.ViewModels;

namespace RoomLocator.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly ValueService _valueService;

        public ValuesController(ValueService valueService)
        {
            _valueService = valueService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<ValueViewModel>> Get()
        {
            return Ok(_valueService.Get());
        }

        [HttpGet("{id}")]
        public ActionResult<ValueViewModel> Get(string id)
        {
            if (!_valueService.TryGetValue(id, out var value))
            {
                return NotFound();
            }

            return Ok(value);
        }

        [HttpPost]
        public ActionResult<ValueViewModel> Post([FromBody] ValueInputModel value)
        {
            var createdValue = _valueService.Create(value);
            return CreatedAtAction(nameof(Get), new {id = createdValue.Id}, createdValue);
        }

        [HttpPut("{id}")]
        public ActionResult<ValueViewModel> Put(string id, [FromBody] ValueInputModel value)
        {
            if (!_valueService.TryGetValue(id, out var existingValue))
            {
                NotFound();
            }

            existingValue.Text = value.Text;

            return Ok(existingValue);
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            return NoContent();
        }
    }
}