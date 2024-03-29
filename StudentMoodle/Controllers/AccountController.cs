﻿using Microsoft.AspNetCore.Mvc;

using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using StudentMoodle.Models;
using Microsoft.EntityFrameworkCore;
using StudentMoodle.Models.Authorization;

namespace StudentMoodle.Controllers
{
    public class AccountController : Controller
    {

        private readonly ApplicationDbContext _context;
        private readonly ILogger<AccountController> _logger;
        public AccountController(ApplicationDbContext userContext, ILogger<AccountController>? logger)
        {
            _context = userContext;
            _logger = logger;
        }

        public async Task<ActionResult> Login()
        {
            ClaimsPrincipal claimUser = HttpContext.User;
            var currentUserName = claimUser.Identity.Name;
            var user = await _context.Users
                .FirstOrDefaultAsync(s => s.Email == currentUserName);
            _logger.LogInformation("Пользователь найден");
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
                    var role = await _context.Roles
                    .FirstAsync(r => r.Id == user.RoleId); ;
                    await Authenticate(modelLogin.Email, role.RoleName); // аутентификация
                    _logger.LogInformation("Аунтификация пройдена");
                    return RedirectToAction("Index", "Home", user);
                }

                ViewData["ValidateMessage"] = "логин или пароль указаны неверно";
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
            _logger.LogInformation("Пользователь вышел");
            return RedirectToAction("Login", "Account");
        }
    }
}
