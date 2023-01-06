using Microsoft.AspNetCore.Mvc;

using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using StudentMoodle.Models;
using Microsoft.EntityFrameworkCore;
using StudentMoodle.Models.Authorization;
using System.Runtime.InteropServices;
using Microsoft.AspNetCore.Identity;

namespace StudentMoodle.Controllers
{
    public class AccountController : Controller
    {

        private readonly UserContext _context;

        public AccountController(UserContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Login()
        {
            ClaimsPrincipal claimUser = HttpContext.User;
            /*var a = HttpContext.User.Claims.Where(x => x.Type == ClaimsIdentity.DefaultNameClaimType).Single().Value;*/
            var currentUserName = claimUser.FindFirst(ClaimTypes.NameIdentifier).Value;
            var user = await _context.Users
                    .FirstOrDefaultAsync(s => s.Id.ToString() == currentUserName);
            if (claimUser.Identity.IsAuthenticated)
                return RedirectToAction("Index", "Home", user);


            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginModel modelLogin)
        {
            if (ModelState.IsValid)
            {
                var user = await _context.Users
                    .FirstOrDefaultAsync(s => s.Email == modelLogin.Email && s.Password == modelLogin.Password);
                if (user != null)
                {
                    await Authenticate(modelLogin.Email); // аутентификация

                    return RedirectToAction("Index", "Home", user);
                }

                ViewData["ValidateMessage"] = "user not found";
                return View();

            }
            return View(modelLogin);
        }

        private async System.Threading.Tasks.Task Authenticate(string userName)
        {
            // создаем один claim
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, userName)
            };
            // создаем объект ClaimsIdentity
            ClaimsIdentity id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
            // установка аутентификационных куки
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
        }

        [HttpGet]
        public async Task<IActionResult> LogOut()
        {

            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Account");
        }
    }
}
