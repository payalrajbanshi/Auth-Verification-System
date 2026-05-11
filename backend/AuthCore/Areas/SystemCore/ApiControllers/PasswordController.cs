using AuthVerification.Core.src.UserFeature.DTOs;
using AuthVerification.Core.src.UserFeature.Exceptions;
using AuthVerification.Core.src.UserFeature.Interfaces;
using AuthVerification.Dbal.DbContexts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;

namespace AuthVerification.Areas.SystemCore.ApiControllers
{
    [ApiController]
    [Area("SystemCore")]
    [Route("api/[area]/password")]

    public class PasswordController : ControllerBase
    {
        private readonly AppDbContext _db;
        private readonly IUserService _userService;
        public PasswordController(AppDbContext db, IUserService userService)
        {
            _db = db;
            _userService = userService;
        }

        [Authorize]
        [HttpPost("change")]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            await _userService.ChangePasswordAsync(dto);
            return Ok(new { message = "Password changed successfully" });
          
        }


        [HttpPost("forgot")]
        public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            await _userService.GeneratePasswordResetByEmailAsync(dto.Email);
            return Ok(new { message = "If the email exist, password reset pin  has been sent!." });
        }

        [HttpPost("reset")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            await _userService.ResetPasswordUsingPinAsync(dto);
            return Ok(new { message = "Password reset successfully. please login again" });

        }

        [Authorize(Roles = "SuperAdmin")]
        [HttpPost("admin-reset")]

        public async Task<IActionResult> AdminResetPassword([FromQuery] long userId, [FromBody] AdminResetPassword dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            await _userService.AdminResetPasswordAsync(userId, dto.newPassword);
            return Ok(new { message = "Password reset succesful" });
        }
    }
}
