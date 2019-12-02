using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RoomLocator.Data.Services;
using RoomLocator.Domain.ViewModels;
using Shared.Extentions;

namespace RoomLocator.Api.Controllers
{
    /// <summary>
    ///     <author>Anders Wiberg Olsen, s165241</author>
    /// </summary>
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [Authorize]
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
            var user = await _userService.GetByStudentId(User.StudentId());

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

        /// <summary>
        ///     <author>Hadi Horani, s144885</author>
        /// </summary>
        [HttpDelete("id")]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult> Delete(string studentId)
        {
            return Ok(await _userService.DeleteUserInfo(studentId));
        }

        /// <summary>
        ///     <author>Hadi Horani, s144885</author>
        /// </summary>
        [HttpDelete("me")]
        public async Task<ActionResult> DeleteCurrentUserInfo()
        {
            var user = await _userService.GetByStudentId(User.StudentId());
            return Ok(await _userService.DeleteUserInfo(user.StudentId));
        }

        /// <summary>
        ///     <author>Hadi Horani, s144885</author>
        /// </summary>
        [HttpPut("id")]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult> UpdateRole(string studentId, string roleName)
        {
            return Ok(await _userService.UpdateRole(studentId, roleName));
        }

        /// <summary>
        /// States whether or not a user has accepted the disclaimer. If the user does not exist, it says false
        ///     <author>Anders Wiberg Olsen, s165241</author>
        /// </summary>
        /// <param name="studentId"></param>
        /// <returns></returns>
        [HttpGet("{studentId}/disclaimer")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(UserDisclaimerViewModel), StatusCodes.Status200OK)]
        public async Task<ActionResult<UserDisclaimerViewModel>> GetUserDisclaimer(string studentId)
        {
            return await _userService.HasAcceptedDisclaimer(studentId);
        }
        
        /// <summary>
        /// Adds a record stating the user has accepted the disclaimer already
        ///     <author>Anders Wiberg Olsen, s165241</author>
        /// </summary>
        /// <returns></returns>
        [HttpPut("me/disclaimer/accept")]
        [ProducesResponseType(typeof(UserDisclaimerViewModel), StatusCodes.Status200OK)]
        public async Task<ActionResult<UserDisclaimerViewModel>> AcceptUserDisclaimer()
        {
            return await _userService.HasAcceptedDisclaimer(User.StudentId());
        }
    }
}
