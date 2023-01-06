using Microsoft.AspNetCore.Mvc;

using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using StudentMoodle.Models;
using Microsoft.EntityFrameworkCore;
using StudentMoodle.Models.Authorization;
using Microsoft.AspNetCore.Identity;

namespace StudentMoodle.Controllers
{
    public class AccountController : Controller
    {

        private readonly UserContext _userContext;
        private readonly RoleContext _roleContext;

        public AccountController(UserContext userContext, RoleContext roleContext)
        {
            _userContext = userContext;
            _roleContext = roleContext;
        }

        /*private readonly UserManager<UserView> _userManager;
        private readonly SignInManager<UserView> _signInManager;

        public AccountController(UserManager<UserView> userManager, SignInManager<UserView> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }*/

        public IActionResult Login()
        {
            ClaimsPrincipal claimUser = HttpContext.User;

            if (claimUser.Identity.IsAuthenticated)
                return RedirectToAction("Index", "User");


            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginModel modelLogin)
        {
            if (ModelState.IsValid)
            {
                var user = await _userContext.Users
                    .FirstOrDefaultAsync(s => s.Email == modelLogin.Email && s.Password == modelLogin.Password);
                var role = await _roleContext.Roles
                    .FirstOrDefaultAsync(r => r.Id == user.RoleId);

                if (user != null)
                {
                    await Authenticate(modelLogin.Email, role.RoleName); // аутентификация

                    return RedirectToAction("Index", "User");
                    //await Authenticate(modelLogin.Email); // аутентификация
                }

                ViewData["ValidateMessage"] = "user not found";
                return View();

            }
            return View(modelLogin);
        }

        private async System.Threading.Tasks.Task Authenticate(string userName, string role)
        {
            // создаем один claim
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, userName),
                new Claim(ClaimTypes.Role, role)
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
