using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RoomLocator.Data.Services;
using RoomLocator.Domain.Models;
using RoomLocator.Domain.ViewModels;

namespace RoomLocator.Api.Controllers
{
    /// <summary>
    /// <author>Anders Wiberg Olsen, s165241</author>
    /// </summary>
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]

    //  [Authorize]
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
            var userId = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
            var user = await _userService.GetByStudentId(userId);

            return user;
        }

        [HttpGet]
//        [Authorize(Roles = "admin")]
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

        /// <summary>
        ///     <author>Hadi Horani, s144885</author>
        /// </summary>
        [HttpDelete("id")]
     //   [Authorize(Roles = "admin")]
        public async Task<ActionResult> Delete(string studentId)
        {
            return Ok(await _userService.DeleteUserInfo(studentId));
        }

        /// <summary>
        ///     <author>Hadi Horani, s144885</author>
        /// </summary>
        [HttpPut("id")]
     //   [Authorize(Roles = "admin")]
        public async Task<ActionResult> UpdateRole(string studentId, string roleName)
        {
            return Ok(await _userService.UpdateRole(studentId, roleName));
        }
    }
}
