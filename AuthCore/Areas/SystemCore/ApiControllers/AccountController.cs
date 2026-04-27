using AuthVerification.Dbal.DbContexts;
using AuthVerification.Dbal.DbModels;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Text;
using static AuthVerification.Core.src.UserFeature.Entities.UserEntity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
namespace AuthVerification.Areas.SystemCore.ApiControllers
{
    [ApiController]
    [Area("SystemCore")]
    [Route("api/[area]/auth")]
    public class AccountController : ControllerBase
    {
        private readonly AppDbContext _db;
        private readonly IConfiguration _configuration;
        public AccountController(AppDbContext db, IConfiguration configuration)
        {
            _db = db;
            _configuration = configuration;
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDTO model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid request");
            }
            var user = await _db.Users.FirstOrDefaultAsync(u => u.Username == model.Username);
            if (user == null)
            {
                return Unauthorized("Invalid Username or Password");
            }
            if (user.Status != UserStatus.Active)
                return Forbid("user is not active");
            //if (!user.IsEmailConfirmed)
            //    return Unauthorized("Invalid not verified");
            if (!user.IsEmailConfirmed && !string.IsNullOrWhiteSpace(user.TempEmail))
            {
                var token = GenerateJwtToken(user);
                return Ok(new LoginResponseDTO
                {
                    UserId = user.UserId,
                    Name = user.Name,
                    Role = user.UserType.ToString(),
                    Token = token,
                    TempEmail = user.TempEmail
                });
            }
            if (string.IsNullOrWhiteSpace(user.PasswordHash) || !BCrypt.Net.BCrypt.Verify(model.Password, user.PasswordHash))
                return Unauthorized("Invalid username or password");
            var jwtToken = GenerateJwtToken(user);
            return Ok(new LoginResponseDTO
            {
                UserId = user.UserId,
                Name = user.Name,
                Role = user.UserType.ToString(),
                Token = jwtToken,

            });
        }
        [HttpPost("logout")]
        public IActionResult Logout()
        {
            return Ok("Logged out successfully");
        }

        private string GenerateJwtToken(UserDbModel user)
        {
            var jwtKey = _configuration["Jwt:Key"];
            if (string.IsNullOrWhiteSpace(jwtKey))
                throw new InvalidOperationException("Jwt key is not configured");
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
                new Claim(ClaimTypes.Name, user.Name),
                new Claim(ClaimTypes.Role, user.UserType.ToString()),


            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddHours(1),
                signingCredentials: creds

                );
            return new JwtSecurityTokenHandler().WriteToken(token);



        }
    }
}
