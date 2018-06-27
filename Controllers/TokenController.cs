using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using StudyJWT_on_ASPNETCore.Model;

namespace StudyJWT_on_ASPNETCore.Controllers
{
    [Route("api/[controller]")]
    public class TokenController : Controller
    {
        private IConfiguration _config;

        public TokenController(IConfiguration config)
        {
            _config = config;
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult CreateToken([FromBody] LoginModel login)
        {
            try
            {
                var user = Database.Database.UserDatabase.Single(x => x.Username == login.Username && x.Password == login.Password);
                var tokenString = BuildToken(user);

                return Ok(new { token = tokenString });
            }
            catch (InvalidOperationException ex)
            {
                return Unauthorized();
            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }
        }

        private string BuildToken(User user)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>()
            {
                // new Claim(JwtRegisteredClaimNames.Jti, Config.Id),
                new Claim(JwtRegisteredClaimNames.Sub, user.Name),
                new Claim(ClaimTypes.Name, user.Name),
                new Claim(ClaimTypes.Role, user.Role)
            };

            var token = new JwtSecurityToken(
                issuer : _config["Jwt:Issuer"],
                audience : _config["Jwt:Audience"],
                claims : claims,
                expires : DateTime.UtcNow.AddDays(7),
                signingCredentials : creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

    }
}