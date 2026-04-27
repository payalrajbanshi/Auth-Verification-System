using AuthVerification.Core.src.OrganizationsFeature.DTOs;
using AuthVerification.Core.src.OrganizationsFeature.Entities;
using AuthVerification.Core.src.OrganizationsFeature.Interfaces;
using AuthVerification.Dbal.DbContexts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;

namespace AuthVerification.Areas.Organization.ApiControllers
{

    [Authorize(Roles = "SuperAdmin")]
    [ApiController]
    [Area("SystemCore")]
    [Route("api/[area]/Organization")]
    public class OrganizationController : ControllerBase
    {
        private readonly AppDbContext _db;
        private readonly IOrganizationService _organizationService;


        public OrganizationController(
            AppDbContext db,
            IOrganizationService organizationService)
        {
            _db = db;

            _organizationService = organizationService;
        }

        [HttpGet("getAll")]
        public async Task<IActionResult> GetAll()
        {
            var org = await _db.Organizations.ToListAsync();
            return Ok(org);

        }


        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] CreateOrganizationDTO dto)
        {

            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var userId = long.Parse(
                User.FindFirstValue(ClaimTypes.NameIdentifier)!
                );
            await _organizationService.CreateOrganizationAsync(dto, userId);
            return Ok(new { message = " organization created successfully" });
        }

        [HttpPost("update")]
        public async Task<IActionResult> Update([FromBody] UpdateOrganizationDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _organizationService.UpdateOrganizationAsync(dto);

            return Ok(new { message = "Organization updated successfully" });
        }

        [HttpPut("{organizationId}/activate")]
        public async Task<IActionResult> Activate(long organizationId)
        {
            await _organizationService.Activate(organizationId);
            return Ok(new { message = "Organization activated successfully" });
        }

        [HttpPut("{organizationId}/deactivate")]
        public async Task<IActionResult> Deactivate(long organizationId)
        {
            await _organizationService.Deactivate(organizationId);
            return Ok(new { message = "Organization deactivated successfully" });
        }

        [HttpPost("verify-email")]
        public async Task<IActionResult> VerifyEmail([FromBody] VerifyOrganizationEmailDTO dto)
        {
            await _organizationService.VerifyEmailAsync(dto.OrganizationId, dto.Pin);
            return Ok(new { message = "Organization email verified successfully" });
        }

        //[HttpPost("verify-mobile")]
        //public async Task<IActionResult> VerifyMobile([FromBody] VerifyOrganizationMobileDTO dto)
        //{
        //    await _organizationService.VerifyMobileAsync(dto.OrganizationId, dto.Pin);
        //    return Ok(new { message = "Organization mobile number verified successfully" });
        //}
    }
}
