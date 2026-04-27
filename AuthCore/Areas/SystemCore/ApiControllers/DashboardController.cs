using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace AuthVerification.Areas.SystemCore.ApiControllers
{
    [ApiController]
    [Area("SystemCore")]
    [Route("api/[area]/dashboard")]
    public class DashboardController : ControllerBase
    {
        [HttpGet("get")]
        [Authorize]
        public IActionResult GetDashboard()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var name = User.FindFirstValue(ClaimTypes.Name);
            var role = User.FindFirstValue(ClaimTypes.Role);

            if (string.IsNullOrEmpty(role))
                return Forbid("Role not found in token");

            string dashboardRole;


            switch (role)
            {
                case "0":
                case "Admin":
                case "SuperAdmin":
                    dashboardRole = "Admin";
                    break;


                case "1":
                case "User":
                case "Reseller":
                case "Organization":
                    dashboardRole = "User";
                    break;

                default:
                    return BadRequest(new { message = "Unknown role" });
            }

            var dashboardType = dashboardRole == "Admin"
                ? "AdminDashboard"
                : "UserDashboard";

            return Ok(new
            {
                DashboardType = dashboardType,
                UserId = userId,
                Username = name,
                Role = dashboardRole,

            });
        }
    }
}
