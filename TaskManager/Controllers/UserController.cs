using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TaskManager.Model;

namespace TaskManager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUser _user;
        private readonly IConfiguration _configuration;

        public UserController(IUser user, IConfiguration configuration)
        {
            _user = user;
            _configuration = configuration;
        }

        [HttpGet("All USers")]
        [Authorize(Roles =Role.Admin)]
        public IActionResult Get()
        {
            return Ok(_user.GetUserBodies());
        }

        [HttpPost("Register")]
        public IActionResult AddUser([FromBody] UserBody user)
        {
            var checkuser = _user.GetUserBodies().FirstOrDefault(x=>x.Name==user.Name && x.Password == user.Password);
            if (checkuser != null)
            {
                return BadRequest("User already exsists");
            }
            _user.Register(user);
            return Ok();
        }
        [HttpPost("Login")]
        public IActionResult Login([FromBody] Login users)
        {
            try
            {
                if (users == null)
                {
                    return BadRequest();
                }
                var Loguser = _user.Login(users);
                if (Loguser == null)
                {
                    return Unauthorized("Invalid username or password");
                }
                string token = GenerateJwtToken(Loguser);
                return Ok(new { Token = token });

            }
            catch
            {
                return BadRequest("Internal Server error");
            }
        }
        private string GenerateJwtToken(UserBody user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Name, user.Name),
            new Claim(ClaimTypes.Role, user.Role)
            };

            var token = new JwtSecurityToken
                (
                claims: claims,
                signingCredentials: credentials,
                expires: DateTime.UtcNow.AddDays(1)
                );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
