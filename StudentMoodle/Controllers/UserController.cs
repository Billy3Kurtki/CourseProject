using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentMoodle.Models;

namespace StudentMoodle.Controllers
{
    [Authorize(Policy = "admin")]
    public class UserController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<AccountController> _logger;

        public UserController(ApplicationDbContext context, ILogger<AccountController> logger)
        {
            _context = context;
            _logger = logger;
        }


        public ActionResult Index()
        {
            return View(_context.Users.ToList()) ;
        }

        [Authorize(Policy = "admin")]
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
        [Authorize(Policy = "admin")]
        public ActionResult Details(int id)
        {
            return View(_context.Users.First(x => x.Id == id));
        }

        
        [Authorize(Policy = "admin")]
        public ActionResult Create()
        {
            ViewBag.Role = _context.Roles.Select(r => r.RoleName);
            return View();
        }

       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(UserView user)
        {
            var groupName = Request.Form["role"].ToString();
            try
            {
                var roles = _context.Roles.First(r => r.RoleName == groupName);
                user.RoleId = roles.Id;
                _context.Users.Add(user);
                _context.SaveChanges();

                if (roles.Id == 2)
                {
                    var lector = new Lector() { Id = user.Id };
                    _context.Lectors.Add(lector);
                }
                
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        
        [Authorize(Policy = "admin")]
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
        [Authorize(Policy = "admin")]
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
        public ActionResult GroupStudent(int? idG)
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

            ViewBag.Student = students.Select(s => s.Email);
            return View(_context.Groups.First(g => g.Id == idG));
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ConnGroupStudent(int? id)
        {
            var studentEmail = Request.Form["student"].ToString();

            if (id == 0|| studentEmail.Length == 0)
                return RedirectToAction(nameof(GroupIndex));

            var group = _context.Groups.First(g => g.Id == id);
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
                return RedirectToAction("GroupStudent", new { idG = id });
            }
            catch
            {
                ViewData["ValidateMessage"] = "user not found";
                return RedirectToAction("GroupStudent", new { idG = id });
            }
        }


        [Authorize(Policy = "admin")]
        public ActionResult DisconnGroupStudent(int? idG)
        {
            var students = _context.Students.Where(s => s.IdGroup == idG).ToList();

            var student = new List<UserView>();
            foreach (var item in students)
                student.Add(_context.Users.First(u => u.Id == item.Id));

            ViewBag.Student = student.Select(s => s.Email);
            return View(_context.Groups.First(g => g.Id == idG));
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DisConnGroupStudent(int? id)
        {
            var studentEmail = Request.Form["student"].ToString();

            if (id == 0 || studentEmail.Length == 0)
                return RedirectToAction(nameof(GroupIndex));

            var group = _context.Groups.First(g => g.Id == id);
            var student = _context.Users.First(g => g.Email == studentEmail);

            Student student1 = new Student()
            {
                Id = student.Id,
                IdGroup = group.Id
            };

            var gd = _context.Group_Disciplines.Where(gd => gd.Idgroup == group.Id);
            try
            {
                _context.Students.Remove(student1);
                await _context.SaveChangesAsync();
                return RedirectToAction("DisconnGroupStudent", new { idG = id });
            }
            catch
            {
                ViewData["ValidateMessage"] = "user not found";
                return RedirectToAction("DisconnGroupStudent", new { idG = id });
            }
        }

        [Authorize(Policy = "admin")]
        public ActionResult GroupIndex()
        {
            return View(_context.Groups.ToList());
        }

        [Authorize(Policy = "admin")]
        public ActionResult GroupCreate()
        {
            return View();
        }

        // POST: StudentController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> GroupCreate(Group group)
        {
            try
            {
                _context.Groups.Add(group);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(GroupIndex));
            }
            catch
            {
                return View();
            }
        }

        // GET: StudentController/Edit/5
        [Authorize(Policy = "admin")]
        public async Task<IActionResult> GroupEdit(int? id)
        {
            if (id == null || _context.Groups == null)
            {
                return NotFound();
            }

            var group = await _context.Groups.FindAsync(id);
            if (group == null)
            {
                return NotFound();
            }
            return View(group);
        }

        // POST: StudentController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> GroupEdit(int id, Group group)
        {
            try
            {
                _context.Groups.Update(group);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(GroupIndex));
            }
            catch
            {
                return View();
            }
        }

        // GET: StudentController/Delete/5
        [Authorize(Policy = "admin")]
        public async Task<IActionResult> GroupDelete(int? id)
        {
            if (id == null || _context.Groups == null)
            {
                return NotFound();
            }

            var group = await _context.Groups
                .FirstOrDefaultAsync(m => m.Id == id);
            if (group == null)
            {
                return NotFound();
            }

            return View(group);
        }

        // POST: StudentController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> GroupDelete(int id, Group group)
        {
            try
            {
                _context.Groups.Remove(group);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(GroupIndex));
            }
            catch
            {
                return View();
            }
        }
    }
}
