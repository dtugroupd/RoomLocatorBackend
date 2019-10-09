using System;
using System.Collections;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace RoomLocator.Data.Services
{
    public class TokenService
    {
        private readonly IConfiguration _configuration;
        private readonly UserService _userService;

        public TokenService(IConfiguration configuration, UserService userService)
        {
            _configuration = configuration;
            _userService = userService;
        }

        public void GenerateToken()
        {
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, "UserId"),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };
        }

        public string GenerateRegisterUserToken(string studentId)
        {
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Sub, studentId),
                new Claim("RegisterUser", studentId), 
            };

            var token = CreateToken(claims, DateTime.Now.AddMinutes(5));
            
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public async Task<string> GenerateUserTokenAsync(string studentId)
        {
            var user = await _userService.GetByStudentId(studentId);

            if (user == null) return null;

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Sub, user.StudentId),
                new Claim(ClaimTypes.Role, "User"),
            };

            var token = CreateToken(claims, DateTime.Now.AddHours(6));
            
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private JwtSecurityToken CreateToken(IEnumerable<Claim> claims, DateTime expiration)
        {
            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_configuration["Auth:SigningKey"] ?? "InsecureSigningKey"));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);;
            
            return new JwtSecurityToken(
                "RoomLocator", 
                "https://se2-webapp04.compute.dtu.dk",
                claims,
                expires: expiration,
                signingCredentials: credentials);
        }
    }
}
