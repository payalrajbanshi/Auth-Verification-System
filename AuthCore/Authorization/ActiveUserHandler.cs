using AuthVerification.Core.src.UserFeature.Entities;
using AuthVerification.Dbal.DbContexts;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace AuthVerification.Authorization
{
    public class ActiveUserHandler : AuthorizationHandler<ActiveUserRequirement>
    {
        private readonly AppDbContext _appDbContext;

        public ActiveUserHandler(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, ActiveUserRequirement requirement)
        {
            var userIdStr = context.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (Guid.TryParse(userIdStr, out var userId))
            {
                var user = await _appDbContext.Users.FindAsync(userId);
                if (user != null && user.Status == UserEntity.UserStatus.Active)
                {
                    context.Succeed(requirement);
                    return;
                }
            }

            context.Fail();
        }
    }
}
