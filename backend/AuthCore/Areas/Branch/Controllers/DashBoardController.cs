using AuthVerification.Dbal.DbContexts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
namespace AuthVerification.Areas.Branch.Controllers
{
    [Area("Branch")]
    [Authorize(Roles = "Branch")]
    public class DashBoardController : Controller
    {

        private readonly AppDbContext _appDbContext;

        public DashBoardController(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<IActionResult> Index()
        {
            var users = await _appDbContext.Users.ToListAsync();
            return View(users);
        }

    }
}
