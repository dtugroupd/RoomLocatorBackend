using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RoomLocator.Data.Services;
using RoomLocator.Domain.InputModels;
using RoomLocator.Domain.ViewModels;

namespace RoomLocator.Api.Controllers
{
    [ApiVersion("1.0")]
    [Authorize]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserService _userService;

        public UserController(UserService userService)
        {
            _userService = userService;
        }

        [HttpGet("me")]
        public async Task<ActionResult<UserViewModel>> GetCurrentUser()
        {
            var userId = User.Claims.FirstOrDefault(x => x.Type == "uid")?.Value;
            var user = await _userService.Get(userId);

            return user;
        }

        [HttpGet]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult<IEnumerable<UserViewModel>>> Get()
        {
            var users = await _userService.Get();
            return Ok(users);
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult<UserViewModel>> Get(string studentId)
        {
            return Ok(await _userService.GetByStudentId(studentId));
        }

        [HttpDelete("id")]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult> Delete(string studentId)
        {
            await _userService.Delete(studentId);
            return NoContent();
        }
    }
}
