using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using StudentMoodle.Models;
using System.Linq;
using System.Security.Claims;

namespace StudentMoodle.Controllers
{
    [Authorize]
    public class TestController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<AccountController> _logger;

        public TestController(ApplicationDbContext context, ILogger<AccountController> logger)
        {
            _context = context;
            _logger = logger;
        }

        // GET: TestController
        public async Task<ActionResult> Index(Test tests)
        {
            ClaimsPrincipal claimUser = HttpContext.User;
            var currentUserName = claimUser.Identity.Name;
            var user = await _context.Users.FirstAsync(s => s.Email == currentUserName);

            var test = await _context.Tests.FirstAsync(t => t.Id == tests.Id);
            var task = _context.Tasks.Where(t => t.idTest == test.Id).ToList();
            var answer = new List<Answer>();

            foreach (var item in task)
            {
                var answers = _context.Answers.Where(a => a.idTask == item.Id);
                foreach (var item2 in answers)
                {
                    answer.Add(item2);
                }
            }
            var model = (
                test,
                task,
                answer,
                user);
            return View("TestForm", model);
        }

        [Authorize(Policy = "lector")]
        public ActionResult TasksIndex(int? idtest, Test tests)
        {
            if (idtest != null)
            {
                tests = _context.Tests.First(t => t.Id == idtest);
            }
            var task = _context.Tasks.Where(t => t.idTest == tests.Id).ToList();
            var model = (
                task,
                tests,
                new Tasks());
            return View(model);
        }

        [Authorize(Policy = "lector")]
        public async Task<ActionResult> TasksCreate(int? testkey)
        {
            if (testkey == null || _context.Tests == null)
            {
                return NotFound();
            }

            var test = await _context.Tests.FindAsync(testkey);

            if (test == null)
            {
                return NotFound();
            }

            var task = new Tasks()
            {
                idTest = test.Id
            };
            return View(task);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> TasksCreate(Tasks tasks)
        {
            try
            {
                _context.Tasks.Add(tasks);
                await _context.SaveChangesAsync();
                return RedirectToAction("TasksIndex", "Test", new { idtest = tasks.idTest });
            }
            catch
            {
                return View();
            }
        }

        [Authorize(Policy = "lector")]
        public ActionResult AnswerIndex(int? idtask, Tasks tasks)
        {
            if (idtask != null)
            {
                tasks = _context.Tasks.First(t => t.Id == idtask);
            }
            var answer = _context.Answers.Where(a => a.idTask == tasks.Id).ToList();
            var model = (
                answer,
                tasks,
                new Answer());
            return View(model);
        }

        [Authorize(Policy = "lector")]
        public async Task<ActionResult> AnswerCreate(int? tascskey)
        {
            if (tascskey == null || _context.Tasks == null)
            {
                return NotFound();
            }

            var tasks = await _context.Tasks.FindAsync(tascskey);

            if (tasks == null)
            {
                return NotFound();
            }

            var answer = new Answer()
            {
                idTask = tasks.Id
            };
            return View(answer);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AnswerCreate(Answer answer)
        {
            try
            {
                _context.Answers.Add(answer);
                await _context.SaveChangesAsync();
                return RedirectToAction("AnswerIndex", "Test", new { idtask = answer.idTask });
            }
            catch
            {
                return View();
            }
        }

        [Authorize(Policy = "lector")]
        public async Task<ActionResult> EditAnswer(int? id)
        {
            if (id == null || _context.Answers == null)
            {
                return NotFound();
            }

            var answer = await _context.Answers.FindAsync(id);
            if (answer == null)
            {
                return NotFound();
            }
            return View(answer);
        }

        // POST: TestController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditAnswer(int? id, Answer answer)
        {
            try
            {
                _context.Answers.Update(answer);
                _context.SaveChanges();
                return RedirectToAction("AnswerIndex", "Test", new { idtask = answer.idTask });
            }
            catch
            {
                return View();
            }
        }

        [Authorize(Policy = "lector")]
        public async Task<IActionResult> AnswerDelete(int? id)
        {
            if (id == null || _context.Answers == null)
            {
                return NotFound();
            }

            var answer = await _context.Answers
                .FirstOrDefaultAsync(m => m.Id == id);
            if (answer == null)
            {
                return NotFound();
            }

            return View(answer);
        }

        // POST: StudentController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AnswerDelete(int id, Answer answer)
        {
            try
            {
                answer = _context.Answers.Find(id);
                var idTask = answer.idTask;
                _context.Answers.Remove(answer);
                await _context.SaveChangesAsync();
                return RedirectToAction("AnswerIndex", "Test", new { idtask = idTask });
            }
            catch
            {
                return View();
            }
        }

        [Authorize(Policy = "lector")]
        public async Task<ActionResult> EditTask(int? id)
        {
            if (id == null || _context.Tasks == null)
            {
                return NotFound();
            }

            var task = await _context.Tasks.FindAsync(id);
            if (task == null)
            {
                return NotFound();
            }
            return View(task);
        }

        // POST: TestController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditTask(int id, Tasks task)
        {
            try
            {
                _context.Tasks.Update(task);
                _context.SaveChanges();
                return RedirectToAction("TasksIndex", "Test", new { idtest = task.idTest });
            }
            catch
            {
                return View();
            }
        }

        [Authorize(Policy = "lector")]
        public async Task<IActionResult> DeleteTask(int? id)
        {
            if (id == null || _context.Tasks == null)
            {
                return NotFound();
            }

            var task = await _context.Tasks
                .FirstOrDefaultAsync(m => m.Id == id);
            if (task == null)
            {
                return NotFound();
            }

            return View(task);
        }

        // POST: StudentController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteTask(int id, Tasks task)
        {
            try
            {

                task = _context.Tasks.Find(id);
                var idTest = task.idTest;
                _context.Tasks.Remove(task);
                await _context.SaveChangesAsync();
                return RedirectToAction("TasksIndex", "Test", new { idtest = idTest });
            }
            catch
            {
                return View();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CheakTest(int idtest, int iduser)
        {
            var tasks = _context.Tasks.Where(t => t.idTest == idtest).ToList();
            //var answer = new List<Answer>();
            int score = 0;
            foreach (var item in tasks)
            {
                bool answer = Request.Form[$"{item.Title}"].ToString() == "True";
                if (answer)
                {
                    score++;
                }
            }

            var test = _context.Tests.First(t => t.Id == idtest);

            var scoreTest = new TestandStudent()
            {
                idtest = idtest,
                idstudent = iduser,
                iddiscipline = test.IdDiscipline,
                score = score,
                passDate = DateTime.Now
            };
            _context.TestandStudents.Add(scoreTest);
            await _context.SaveChangesAsync();
            return RedirectToAction("TestDetails", "Discipline", new { idtest = idtest });
        }

        [Authorize(Policy = "lector")]
        public IActionResult PassedStudent(int idtest, int? idgroup)
        {
            var groupsDownList = _context.Groups.ToList();
            ViewBag.Group = groupsDownList.Select(g => g.Title);
            var test = _context.Tests.First(t => t.Id == idtest);

            var passedStudent = new List<TestandStudent>();
            var liststudents = new List<UserView>();

            var group = new Models.Group();

            try
            {
                group = _context.Groups.First(g => g.Id == idgroup);
                var students = _context.Students.Where(s => s.IdGroup == idgroup).ToList();
                passedStudent = _context.TestandStudents.Where(ps => ps.idtest == idtest).ToList();
                foreach (var item in passedStudent)
                {
                    foreach (var student in students)
                    {
                        if (item.idstudent == student.Id)
                        {
                            liststudents.Add(_context.Users.First(s => s.Id == student.Id));
                        }
                    }
                }

            }
            catch
            {
                passedStudent = _context.TestandStudents.Where(ps => ps.idtest == idtest).ToList();
                foreach (var item in passedStudent)
                {
                    liststudents.Add(_context.Users.First(s => s.Id == item.idstudent));
                }
            }

            var model = (
                liststudents,
                passedStudent,
                new UserView(),
                test,
                group);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ResultOfTestByGroup(int idtest)
        {
            var groupTitle = Request.Form["group"].ToString();
            var test = _context.Tests.First(t => t.Id == idtest);

            if (groupTitle.Length != 0)
            {
                int groupId = _context.Groups.First(g => g.Title == groupTitle).Id;
                return RedirectToAction("PassedStudent", new { idtest = test.Id, idgroup = groupId });
            }
            else
            {
                return RedirectToAction("PassedStudent", new { idtest = test.Id });
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> OpenTest(int iduser, int idtest)
        {
            var testandstudent = _context.TestandStudents.First(ts => ts.idtest == idtest && ts.idstudent == iduser);
            _context.TestandStudents.Remove(testandstudent);
            await _context.SaveChangesAsync();
            return RedirectToAction("PassedStudent", new { idtest = idtest });
        }
        
    }
}