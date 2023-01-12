﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentMoodle.Models;

namespace StudentMoodle.Controllers
{
    public class TestController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TestController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: TestController
        public async Task<ActionResult> Index(Test tests)
        {
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
                answer);
            return View("TestForm", model);
        }

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
        // GET: TestController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: TestController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TestController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
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

        // GET: TestController/Edit/5
        public async Task<ActionResult> EditAnswer(int id)
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
        public ActionResult EditAnswer(int id, Answer answer)
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
                return RedirectToAction("AnswerIndex", "Test", new { id = idTask });
            }
            catch
            {
                return View();
            }
        }

        public async Task<ActionResult> EditTask(int id)
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
                return RedirectToAction("TasksIndex", "Test", new { id = idTest });
            }
            catch
            {
                return View();
            }
        }
    }
}