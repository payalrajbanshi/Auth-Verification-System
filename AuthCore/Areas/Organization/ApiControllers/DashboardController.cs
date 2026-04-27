using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace AuthVerification.Areas.Organization.ApiControllers
{
    [ApiController]
    [Area("SystemCore")]
    [Route("api/[area]/organization-dashboard")]
    public class OrganizationDashboardController : ControllerBase
    {
        [HttpGet("get")]
        [Authorize]
        public IActionResult GetOrganizationDashboard()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var name = User.FindFirstValue(ClaimTypes.Name);
            var role = User.FindFirstValue(ClaimTypes.Role);
            var organizationId = User.FindFirstValue("OrganizationId");

            if (string.IsNullOrEmpty(role))
                return Forbid("Role not found in token");

            if (role != "Organization" && role != "Reseller" && role != "User")
                return Forbid("Unauthorized role for organization dashboard");

            return Ok(new
            {
                DashboardType = "OrganizationDashboard",
                UserId = userId,
                Username = name,
                Role = role,
                OrganizationId = organizationId
            });
        }
    }
}
