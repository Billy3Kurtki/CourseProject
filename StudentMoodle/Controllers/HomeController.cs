using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentMoodle.Models;
using System.Diagnostics;
using System.Security.Claims;

namespace StudentMoodle.Controllers
{
    [Authorize]
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
            if(user.RoleId == 2)
                disciplines = _context.Disciplines.Where(d => d.IdLector == user.Id).ToList();
            if(user.RoleId == 1)
            {
                var student = _context.Students.First(s => s.Id == user.Id);
                var group_discplines = _context.Group_Disciplines.Where(g => g.Idgroup == student.IdGroup).ToList();
                foreach (var item in group_discplines)
                {
                    disciplines.Add(_context.Disciplines.First(d => d.Id == item.Iddiscipline));
                }
            }
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