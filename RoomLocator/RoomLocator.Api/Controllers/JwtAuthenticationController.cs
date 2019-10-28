using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using RoomLocator.Domain.ViewModels;

namespace RoomLocator.Api.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class JwtAuthenticationController : Controller
    {

        private readonly UserManager<IdentityUser> _userManager;
        private readonly IConfiguration _configuration;

        public JwtAuthenticationController(UserManager<IdentityUser> userManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _configuration = configuration;
        }


        //login endpoints
        [Route("login")]
        [HttpPost]
        public async Task<ActionResult> Login([FromBody] LoginViewModel login)
        {
            var user = await _userManager.FindByNameAsync(login.UserName);
            if (user != null && await _userManager.CheckPasswordAsync(user, login.Password))
            {
                var claim = new[]
                {
                    new Claim(JwtRegisteredClaimNames.Sub, user.UserName)
                };


                var siginKey = new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(_configuration["Jwtsettings:Secret"]) //Jwtsettings:Secret
                    );
                int expiryInMinutes = Convert.ToInt32(_configuration["Jwtsettings:ExpiryInMinutes"]);

                var token = new JwtSecurityToken(
                    issuer: _configuration["Jwtsettings:Site"],
                    audience: _configuration["Jwtsettings:Site"],
                    expires: DateTime.UtcNow.AddMinutes(expiryInMinutes),
                    signingCredentials: new SigningCredentials(siginKey, SecurityAlgorithms.HmacSha256)
                    );

                return Ok(
                           new
                           {
                               token = new JwtSecurityTokenHandler().WriteToken(token),
                               expiration = token.ValidTo
                           });
            }
            return Unauthorized();

        }
    }
}