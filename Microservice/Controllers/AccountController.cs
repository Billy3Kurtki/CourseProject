using Microsoft.AspNetCore.Mvc;

using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microservice.Models.Authorization;
using Microservice.Models;

namespace Microservice.Controllers
{
    public class AccountController : Controller
    {

        private readonly StudentContext _context;

        public AccountController(StudentContext context)
        {
            _context = context;
        }

        public IActionResult Login()
        {
            ClaimsPrincipal claimUser = HttpContext.User;

            if (claimUser.Identity.IsAuthenticated)
                return RedirectToAction("Index", "Students");


            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginModel modelLogin)
        {

            if (modelLogin.Email == "user@example.com" &&
                modelLogin.Password == "123"
                )
            {
                List<Claim> claims = new List<Claim>() {
                    new Claim(ClaimTypes.NameIdentifier, modelLogin.Email),
                    new Claim("OtherProperties","Example Role")

                };

                ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims,
                    CookieAuthenticationDefaults.AuthenticationScheme);

                AuthenticationProperties properties = new AuthenticationProperties()
                {

                    AllowRefresh = true,
                    IsPersistent = modelLogin.KeepLoggedIn
                };

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity), properties);

                return RedirectToAction("Index", "Students");
            }



            ViewData["ValidateMessage"] = "user not found";
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> LogOut()
        {

            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Account");
        }
    }
}
