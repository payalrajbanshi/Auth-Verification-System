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
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordViewModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var userId = long.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var dto = new ChangePasswordDTO
            {
                userId = userId,
                currentPassword = model.CurrentPassword,
                newPassword = model.NewPassword,
                confirmPassword = model.ConfirmNewPassword
            };
            try
            {
                await _userService.ChangePasswordAsync(dto);
                return Ok(new { message = "Password changed successfully" });
            }
            catch (UsernameOrPasswordDoesNotMatchException)
            {

                return BadRequest(new { message = "Current password is incorrect" });
            }
        }


        [HttpPost("forgot")]
        public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordViewModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            await _userService.GeneratePasswordResetByEmailAsync(model.Email);
            return Ok(new { message = "If the email exist, password reset pin  has been sent!." });
        }

        [HttpPost("reset")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordViewModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            if (model.NewPassword != model.ConfirmPassword)
            {
                ModelState.AddModelError("", "Password doesnot match");
                return BadRequest(ModelState);
            }
            try
            {
                var dto = new ResetPasswordDTO
                {
                    userId = model.UserId,
                    email = model.Email,
                    pin = model.Pin,
                    newPassword = model.NewPassword,
                };
                await _userService.ResetPasswordUsingPinAsync(dto);

                return Ok(new { message = "Password reset successfully. Please Login" });
            }
            catch (ValidationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }

        }

        [Authorize(Roles = "SuperAdmin")]
        [HttpPost("admin-reset")]

        public async Task<IActionResult> AdminResetPassword([FromQuery] long userId, [FromBody] ResetPasswordAdminViewModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            await _userService.AdminResetPasswordAsync(userId, model.NewPassword);
            return Ok(new { message = "Password reset succesful" });
        }
    }
}
