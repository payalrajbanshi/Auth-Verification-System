using AuthVerification.Core.src.UserFeature.Interfaces;
using AuthVerification.Dbal.DbContexts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
namespace AuthVerification.Areas.SystemCore.ApiControllers
{
    [ApiController]
    [Area("SystemCore")]
    [Route("api/[area]/verification")]

    public class VerificationController : ControllerBase
    {
        private readonly AppDbContext _db;
        private readonly IUserService _userService;
        public VerificationController(AppDbContext db, IUserService userService)
        {
            _db = db;
            _userService = userService;
        }
        [HttpGet("Email")]
        public async Task<IActionResult> VerifyEmail(long tempEmail)
        {
            // var user = await _db.Users.FirstOrDefaultAsync(u => u.UserId == userId);
            var user = await _db.Users.FindAsync(tempEmail);
            if (user == null)

                return NotFound("new {message= $user with Id{userId} not found");
            var model = new VerifyEmailViewModel
            {
                //UserId = user.UserId,
                TempEmail = user.TempEmail,
            };
            return Ok(model);

        }
        [HttpPost("Email")]
        public async Task<IActionResult> VerifyEmail(VerifyEmailViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var user = await _db.Users.FirstOrDefaultAsync(u => u.TempEmail != null && u.TempEmail == model.TempEmail);
            if (user == null)
                return NotFound(new { message = "User not found or temp email mismatch" });

            if (string.IsNullOrWhiteSpace(user.EmailVerificationPin) || user.EmailVerificationPin != model.Pin)
                return BadRequest(new { message = "Invalid pin entered" });
            user.Email = user.TempEmail;
            user.TempEmail = null;
            user.EmailVerificationPin = null;
            user.IsEmailConfirmed = true;

            await _db.SaveChangesAsync();
            return Ok(new { message = "email verified successfully!" });

        }

        [HttpGet("success")]
        public IActionResult Success()
        {
            return Ok(new { message = "Verification successful!" });
        }
    }
}
