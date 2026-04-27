using AuthVerification.Core.src.UserFeature.DTOs;
using AuthVerification.Core.src.UserFeature.Entities;
using AuthVerification.Core.src.UserFeature.Interfaces;
using AuthVerification.Dbal.DbContexts;
using AuthVerification.Dbal.DbModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
namespace AuthVerification.Areas.SystemCore.ApiControllers
{
    [Authorize(Roles = "SuperAdmin")]
    [ApiController]
    [Area("SystemCore")]
    [Route("api/[area]/User")]
    public class UsersController : ControllerBase
    {
        private readonly AppDbContext _db;
        private readonly IUserService _userService;
        private readonly IPasswordHasher<UserDbModel> _passwordHasher;


        public UsersController(AppDbContext db, IUserService userService)
        {
            _db = db;
            _userService = userService;
        }
        [HttpGet("getAll")]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _db.Users.ToListAsync();
            return Ok(users);

        }
        [HttpPost("create")]
        public async Task<IActionResult> CreateUser([FromBody] CreateUserDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            await _userService.CreateUserAsync(dto);
            return Ok(new { message = " User created successfully" });
        }

        [HttpPost("update")]
        public async Task<IActionResult> UpdateUser(long userId, [FromBody] UpdateViewModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var user = await _db.Users.FindAsync(userId);
            if (user == null)
                return NotFound(new { message = "User not found" });
            user.Name = model.Name;
            user.Username = model.Username;
            user.Email = model.Email;
            user.MobileNo = model.MobileNo;
            // user.UserType = model.UserType;
            user.UserType = Enum.Parse<UserEntity.UserRole>(model.UserType);


            await _db.SaveChangesAsync();
            return Ok(new { message = "user updated successfully" });

        }
        [HttpPut("{userId}/activate")]
        public async Task<IActionResult> ActivateUser([FromRoute] long userId)
        {
            var user = await _db.Users.FindAsync(userId);
            if (user == null)
                return NotFound(new { message = "User Not Found!" });
            user.Status = UserEntity.UserStatus.Active;
            await _db.SaveChangesAsync();
            return Ok(new { message = "User activated successfully!" });
        }
        [HttpPut("{userId}/deactivate")]
        public async Task<IActionResult> DeactivateUser([FromRoute] long userId)
        {
            var user = await _db.Users.FindAsync(userId);
            if (user == null)
                return NotFound(new { message = "User Not found!" });
            user.Status = UserEntity.UserStatus.InActive; ;
            await _db.SaveChangesAsync();
            return Ok(new { message = "User deactivated successfully!" });
        }

    }
}
