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
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserService _userService;

        public UserController(UserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        [Authorize(Roles = "User")]
        public async Task<ActionResult<IEnumerable<UserViewModel>>> Get()
        {
            var users = await _userService.Get();
            return Ok(users);
        }

        [HttpPost]
        [Authorize(Policy = "RegisterUser")]
        public async Task<ActionResult<UserViewModel>> Create(UserInputModel input)
        {
            var userToRegister = User.Claims.FirstOrDefault(x => x.Type == "RegisterUser")?.Value;

            if (userToRegister == null)
            {
                return Forbid("Claim 'RegisterUser' is required to register new users");
            }

            var createdUser = await _userService.Create(userToRegister, input);
            return createdUser;
        }
    }
}