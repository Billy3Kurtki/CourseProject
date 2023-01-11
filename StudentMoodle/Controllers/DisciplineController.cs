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
            var disciplines = new List<Discipline>();
            ClaimsPrincipal claimUser = HttpContext.User;
            var currentUserName = claimUser.Identity.Name;
            var user = _context.Users.First(u => u.Email == currentUserName);
            if (user.RoleId == 2)
            {
                var lector = _context.Lectors.First(l => l.Id == user.Id);
                if (lector != null)
                {
                    disciplines = _context.Disciplines.Where(d => d.IdLector == user.Id).ToList();
                }
            }

            if(user.RoleId == 1)
            {
                var student = _context.Students.First(s => s.Id == user.Id);
                var group_discplines = _context.Group_Disciplines.Where(g => g.Idgroup == student.IdGroup).ToList();
                foreach (var item in group_discplines)
                {
                    disciplines.Add(_context.Disciplines.First(d => d.Id == item.Iddiscipline));
                }
            }
            var discipline1 = new Discipline();
            var model = (
                disciplines,
                user,
                discipline1
                );
            return View(model);

        }

        [Authorize(Policy = "lector")]
        public async Task<ActionResult> Create()
        {
            return View();
        }

        // POST: HomeController1/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Discipline discipline)
        {
            try
            {
                _context.Disciplines.Add(discipline);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Discipline");
            }
            catch
            {
                return View();
            }
        }
        public ActionResult IndexDiscipline(Discipline discipline)
        {
            var labWorks = _context.LabWorks.Where(l => l.IdDiscipline == discipline.Id).ToList();
            var tests = _context.Tests.Where(t => t.IdDiscipline == discipline.Id).ToList();
            ClaimsPrincipal claimUser = HttpContext.User;
            var currentUserName = claimUser.Identity.Name;
            var user = _context.Users.First(u => u.Email == currentUserName);
            var model = (
                labWorks,
                user,
                discipline,
                tests);
            return View(model);
        }

        // GET: HomeController1/Details/5
        public ActionResult Details(int id)
        {
            return View(_context.Disciplines.First(x => x.Id == id));
        }

        // GET: HomeController1/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            if (id == null || _context.Disciplines == null)
            {
                return NotFound();
            }

            var disciplines = await _context.Disciplines.FindAsync(id);
            if (disciplines == null)
            {
                return NotFound();
            }
            return View(disciplines);
        }

        // POST: HomeController1/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int? id, Discipline discipline)
        {
            try
            {
                _context.Disciplines.Update(discipline);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Discipline");
            }
            catch
            {
                return View();
            }
        }

        // GET: HomeController1/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            if (id == null || _context.Disciplines == null)
            {
                return NotFound();
            }

            var discipline = await _context.Disciplines
                .FirstOrDefaultAsync(m => m.Id == id);
            if (discipline == null)
            {
                return NotFound();
            }

            return View(discipline);
        }

        // POST: HomeController1/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int? id, Discipline discipline)
        {
            try
            {
                _context.Disciplines.Remove(discipline);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Discipline");
            }
            catch
            {
                return View();
            }
        }

        // GET: HomeController1/Create
        [Authorize(Policy = "lector")]
        public async Task<ActionResult> CreateTest()
        {
            //var model = new Discipline();
            //var disciplines = _context.Disciplines.ToList();
            
            //var disciplines = await _context.Disciplines.ToListAsync();
            //ViewBag.Disciplines = new Microsoft.AspNetCore.Mvc.Rendering.SelectList(disciplines, "iddiscipline", "title");
            return View("~/Views/Discipline/FormsCreate/TestCreate/CreateTest.cshtml");
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

        public ActionResult TestDetails(Test test)
        {
            return View("~/Views/Discipline/FormsCreate/TestCreate/TestDetails.cshtml", test);
        }

        public async Task<IActionResult> TestEdit(int? id)
        {
            if (id == null || _context.Tests == null)
            {
                return NotFound();
            }

            var test = await _context.Tests.FindAsync(id);
            if (test == null)
            {
                return NotFound();
            }
            return View("~/Views/Discipline/FormsCreate/TestCreate/TestEdit.cshtml", test);
        }

        // POST: StudentController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> TestEdit(int id, Test test)
        {
            try
            {
                _context.Tests.Update(test);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Discipline");
            }
            catch
            {
                return View();
            }
        }

        public async Task<IActionResult> TestDelete(int? id)
        {
            if (id == null || _context.Tests == null)
            {
                return NotFound();
            }

            var labWork = await _context.Tests
                .FirstOrDefaultAsync(m => m.Id == id);
            if (labWork == null)
            {
                return NotFound();
            }

            return View("~/Views/Discipline/FormsCreate/TestCreate/TestDelete.cshtml", labWork);
        }

        // POST: StudentController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> TestDelete(int id, Test test)
        {
            try
            {
                _context.Tests.Remove(test);
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
            return View("~/Views/Discipline/FormsCreate/LabWorkCreate/CreateLabWork.cshtml");
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

        public ActionResult LabWorkDetails(LabWork labWork)
        {
            return View("~/Views/Discipline/FormsCreate/LabWorkCreate/LabWorkDetails.cshtml", labWork);
        }

        public async Task<IActionResult> LabWorkEdit(int? id)
        {
            if (id == null || _context.LabWorks == null)
            {
                return NotFound();
            }

            var labWork = await _context.LabWorks.FindAsync(id);
            if (labWork == null)
            {
                return NotFound();
            }
            return View("~/Views/Discipline/FormsCreate/LabWorkCreate/LabWorkEdit.cshtml", labWork);
        }

        // POST: StudentController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LabWorkEdit(int id, LabWork labWork)
        {
            try
            {
                _context.LabWorks.Update(labWork);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Discipline");
            }
            catch
            {
                return View();
            }
        }

        public async Task<IActionResult> LabWorkDelete(int? id)
        {
            if (id == null || _context.LabWorks == null)
            {
                return NotFound();
            }

            var labWork = await _context.LabWorks
                .FirstOrDefaultAsync(m => m.Id == id);
            if (labWork == null)
            {
                return NotFound();
            }

            return View("~/Views/Discipline/FormsCreate/LabWorkCreate/LabWorkDelete.cshtml", labWork);
        }

        // POST: StudentController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LabWorkDelete(int id, LabWork labWork)
        {
            try
            {
                _context.LabWorks.Remove(labWork);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Discipline");
            }
            catch
            {
                return View();
            }
        }
    }
}
