using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentMoodle.Models;
using System.Security.Claims;

namespace StudentMoodle.Controllers
{
    [Authorize]
    public class DisciplineController : Controller
    {
        // GET: HomeController1

        private readonly ApplicationDbContext _context;

        public DisciplineController(ApplicationDbContext context)
        {
            _context = context;
        }

        public ActionResult Index()
        {
            ClaimsPrincipal claimUser = HttpContext.User;
            var currentUserName = claimUser.Identity.Name;
            var user = _context.Users.First(u => u.Email == currentUserName);
            var lector = _context.Lectors.First(l => l.Id == user.Id);
            if (lector != null)
            {
                var disciplines = _context.Disciplines.Where(d => d.IdLector == user.Id).ToList();
                return View(disciplines);
            }

            return RedirectToAction("Index", "Home");
            
        }
        public ActionResult IndexMMGO()
        {
            return View();
        }

        public ActionResult IndexTP()
        {
            return View();
        }

        public ActionResult IndexRPS()
        {
            return View();
        }

        // GET: HomeController1/Details/5
        public ActionResult Details(int id)
        {
            return View(_context.Disciplines.First(x => x.Id == id));
        }

        // GET: HomeController1/Create
        [Authorize(Policy = "lector")]
        public async Task<ActionResult> CreateTest()
        {
            //var model = new Discipline();
            //var disciplines = _context.Disciplines.ToList();
            
            //var disciplines = await _context.Disciplines.ToListAsync();
            //ViewBag.Disciplines = new Microsoft.AspNetCore.Mvc.Rendering.SelectList(disciplines, "iddiscipline", "title");
            return View("~/Views/Discipline/FormsCreate/CreateTest.cshtml");
        }

        // POST: HomeController1/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateTest(Test test)
        {
            try
            {
                _context.Tests.Add(test);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Discipline");
            }
            catch
            {
                return View();
            }
        }

        [Authorize(Policy = "lector")]
        public async Task<ActionResult> CreateLabWork()
        {
            return View("~/Views/Discipline/FormsCreate/CreateLabWork.cshtml");
        }

        // POST: HomeController1/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateLabWork(LabWork labWork)
        {
            try
            {
                _context.LabWorks.Add(labWork);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Discipline");
            }
            catch
            {
                return View();
            }
        }

        // GET: HomeController1/Edit/5
        /*public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: HomeController1/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: HomeController1/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: HomeController1/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }*/
    }
}
