using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentMoodle.Models;
using System.Diagnostics;
using System.Security.Claims;

namespace StudentMoodle.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;

        }

        public IActionResult Index(UserView user)
        {
            var disciplines = new List<Discipline>();
            disciplines = _context.Disciplines.Where(d => d.IdLector == user.Id).ToList();
            var model = (
                user,
                disciplines);
            return View("Index", model);
        }

        public async Task<ActionResult> Index2()
        {
            ClaimsPrincipal claimUser = HttpContext.User;
            var currentUserName = claimUser.Identity.Name;
            var user = await _context.Users
                    .FirstAsync(s => s.Email == currentUserName);

            if (claimUser.Identity.IsAuthenticated)
                return RedirectToAction("Index", "Home", user);

            return RedirectToAction("Login", "Account");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}