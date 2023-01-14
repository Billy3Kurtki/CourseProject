using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentMoodle.Models;

namespace StudentMoodle.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        private readonly ApplicationDbContext _context;

        public UserController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: StudentController
        public ActionResult Index()
        {
            return View(_context.Users.ToList()) ;
        }

        public ActionResult IndexStudent()
        {
            return View(_context.Users.Where(x => x.RoleId == 1).ToList());
        }

        [Authorize(Policy = "admin")]
        public ActionResult IndexLector()
        {
            return View(_context.Users.Where(x => x.RoleId == 2).ToList());
        }

        // GET: StudentController/Details/5
        public ActionResult Details(int id)
        {
            return View(_context.Users.First(x => x.Id == id));
        }

        // GET: StudentController/Create
        [Authorize(Policy = "admin")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: StudentController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(UserView user)
        {
            try
            {
                _context.Users.Add(user);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: StudentController/Edit/5
        [Authorize(Policy = "lector")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Users == null)
            {
                return NotFound();
            }

            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }

        // POST: StudentController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, UserView user)
        {
            try
            {
                _context.Users.Update(user);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: StudentController/Delete/5
        [Authorize(Policy = "lector")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Users == null)
            {
                return NotFound();
            }

            var user = await _context.Users
                .FirstOrDefaultAsync(m => m.Id == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // POST: StudentController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id, UserView user)
        {
            try
            {
                _context.Users.Remove(user);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        [Authorize(Policy = "admin")]
        public ActionResult GroupStudent()
        {
            
            var studentWithGr = _context.Students.ToList();
            var idStudentwithGr = new List<int>();

            var student = _context.Users.Where(s => s.RoleId == 1);
            var idStudent = new List<int>();

            foreach (var item in studentWithGr)
                idStudentwithGr.Add(item.Id);

            foreach (var item in student)
                idStudent.Add(item.Id);

            List<int> listDontConnStudent = idStudent.Except(idStudentwithGr).ToList();
            var students = new List<UserView>();

            foreach (var item in listDontConnStudent)
                students.Add(_context.Users.First(s => s.Id == item));

            ViewBag.Group = _context.Groups.Select(g => g.Title);
            ViewBag.Student = students.Select(s => s.Email);
            return View();
        }

        // POST: StudentController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ConnGroupStudent()
        {
            var groupName = Request.Form["group"].ToString();
            var studentEmail = Request.Form["student"].ToString();

            if (groupName.Length == 0|| groupName.Length == 0)
                return RedirectToAction(nameof(GroupStudent));

            var group = _context.Groups.First(g => g.Title == groupName);
            var student = _context.Users.First(g => g.Email == studentEmail);

            Student student1 = new Student()
            {
                Id = student.Id,
                IdGroup = group.Id
            };

            var gd = _context.Group_Disciplines.Where(gd => gd.Idgroup == group.Id);
            try
            {
                _context.Students.Add(student1);
                _context.SaveChanges();
                foreach (var item in gd)
                {
                    var score = new Score()
                    {
                        userId = student.Id,
                        disciplineId = item.Iddiscipline,
                        score = 0
                    };
                    _context.Scores.Add(score);
                }

                
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(GroupStudent));
            }
            catch
            {
                return RedirectToAction(nameof(GroupStudent));
            }
        }
    }
}
