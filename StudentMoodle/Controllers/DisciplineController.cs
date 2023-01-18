using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentMoodle.Models;
using System.Security.Claims;
using System.IO;
using System.Drawing.Drawing2D;
using static System.Formats.Asn1.AsnWriter;
using System.Linq;
using System.Text.RegularExpressions;

namespace StudentMoodle.Controllers
{
    [Authorize]
    public class DisciplineController : Controller
    {
        // GET: HomeController1

        private readonly ApplicationDbContext _context;
        IWebHostEnvironment _appEnvironment;
        private readonly ILogger<AccountController> _logger;
        public DisciplineController(ApplicationDbContext context, IWebHostEnvironment appEnvironment, ILogger<AccountController> logger)
        {
            _context = context;
            _appEnvironment = appEnvironment;
            _logger = logger;
        }

        public ActionResult Index()
        {
            var disciplines = new List<Discipline>();
            ClaimsPrincipal claimUser = HttpContext.User;
            var currentUserName = claimUser.Identity.Name;
            var user = _context.Users.First(u => u.Email == currentUserName);
            if (user.RoleId == 2)
            {
                var lector = _context.Lectors.ToList().SingleOrDefault(l => l.Id == user.Id);
                if (lector != null)
                {
                    disciplines = _context.Disciplines.Where(d => d.IdLector == user.Id).ToList();
                }
            }

            if (user.RoleId == 1)
            {
                var student = _context.Students.ToList().SingleOrDefault(s => s.Id == user.Id);
                if (student != null)
                {
                    var group_discplines = _context.Group_Disciplines.Where(g => g.Idgroup == student.IdGroup).ToList();
                    foreach (var item in group_discplines)
                    {
                        disciplines.Add(_context.Disciplines.First(d => d.Id == item.Iddiscipline));
                    }
                }
            }
            _logger.LogInformation("Пользователь открыл главную страницу");
            var discipline1 = new Discipline();
            var model = (
            disciplines,
            user,
            discipline1
            );
            return View(model);

        }

        [Authorize(Policy = "lector")]
        public async Task<ActionResult> Create(int? lkey)
        {
            if (lkey == null || _context.Lectors == null)
            {
                return NotFound();
            }

            var lector = await _context.Lectors.FindAsync(lkey);

            if (lector == null)
            {
                return NotFound();
            }

            var discipline = new Discipline()
            {
                IdLector = lector.Id
            };
            _logger.LogInformation("Дисциплина создана");
            return View(discipline);
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
        public ActionResult IndexDiscipline(int? id, Discipline discipline)
        {
            if (id != null)
            {
                discipline = _context.Disciplines.First(d => d.Id == id);
            }
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
        public ActionResult Details(int? id)
        {
            return View(_context.Disciplines.First(x => x.Id == id));
        }

        // GET: HomeController1/Edit/5
        [Authorize(Policy = "lector")]
        public async Task<ActionResult> Edit(int? id)
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
        [Authorize(Policy = "lector")]
        public async Task<ActionResult> Delete(int? id)
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
        public async Task<ActionResult> CreateTest(int? dkey)
        {
            if (dkey == null || _context.Disciplines == null)
            {
                return NotFound();
            }

            var disciplines = await _context.Disciplines.FindAsync(dkey);

            if (disciplines == null)
            {
                return NotFound();
            }

            var test = new Test()
            {
                IdDiscipline = disciplines.Id
            };

            return View("~/Views/Discipline/FormsCreate/TestCreate/CreateTest.cshtml", test);
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
                return RedirectToAction("IndexDiscipline", "Discipline", new { id = test.IdDiscipline });
            }
            catch
            {
                return View();
            }
        }

        public async Task<ActionResult> TestDetails(int? idtest, Test test)
        {
            ClaimsPrincipal claimUser = HttpContext.User;
            var currentUserName = claimUser.Identity.Name;
            var user = await _context.Users.FirstAsync(s => s.Email == currentUserName);

            if (idtest != null)
            {
                test = _context.Tests.First(d => d.Id == idtest);
            }

            if (test.status != Test.Status.Close && DateTime.Now > test.DeadLine)
            {
                test.status = Test.Status.Close;
                _context.Tests.Update(test);
                await _context.SaveChangesAsync();
            }
            try
            {
                var scoreTest = _context.TestandStudents.First(s => s.idtest == idtest && s.idstudent == user.Id);
                var model = (
                test,
                user,
                scoreTest);
                return View("~/Views/Discipline/FormsCreate/TestCreate/TestDetails.cshtml", model);
            }
            catch
            {
                var model = (
                test,
                user,
                new TestandStudent());
                return View("~/Views/Discipline/FormsCreate/TestCreate/TestDetails.cshtml", model);
            }
            
        }
        [Authorize(Policy = "lector")]
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
                return RedirectToAction("IndexDiscipline", "Discipline", new { id = test.IdDiscipline });
            }
            catch
            {
                return View();
            }
        }

        [Authorize(Policy = "lector")]
        public async Task<IActionResult> TestDelete(int? id)
        {
            if (id == null || _context.Tests == null)
            {
                return NotFound();
            }

            var test = await _context.Tests
            .FirstOrDefaultAsync(m => m.Id == id);
            if (test == null)
            {
                return NotFound();
            }

            return View("~/Views/Discipline/FormsCreate/TestCreate/TestDelete.cshtml", test);
        }

        // POST: StudentController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> TestDelete(int id, Test test)
        {
            try
            {
                test = _context.Tests.Find(id);
                var idDiscipline = test.IdDiscipline;
                _context.Tests.Remove(test);
                await _context.SaveChangesAsync();
                return RedirectToAction("IndexDiscipline", "Discipline", new { id = idDiscipline });
            }
            catch
            {
                return View();
            }
        }

        [Authorize(Policy = "lector")]
        public async Task<ActionResult> CreateLabWork(int? dkey)
        {
            if (dkey == null || _context.Disciplines == null)
            {
                return NotFound();
            }

            var disciplines = await _context.Disciplines.FindAsync(dkey);

            if (disciplines == null)
            {
                return NotFound();
            }

            var labwork = new LabWork()
            {
                IdDiscipline = disciplines.Id
            };

            return View("~/Views/Discipline/FormsCreate/LabWorkCreate/CreateLabWork.cshtml", labwork);
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
                return RedirectToAction("IndexDiscipline", "Discipline", new { id = labWork.IdDiscipline });
            }
            catch
            {
                return View();
            }
        }

        public async Task<ActionResult> LabWorkDetails(LabWork labWork)
        {
            if (labWork.status != LabWork.Status.Close && DateTime.Now > labWork.DeadLine)
            {
                labWork.status = LabWork.Status.Close;
                _context.LabWorks.Update(labWork);
                await _context.SaveChangesAsync();
            }
            ClaimsPrincipal claimUser = HttpContext.User;
            var currentUserName = claimUser.Identity.Name;
            var user = _context.Users.First(u => u.Email == currentUserName);
            var discipline = _context.Disciplines.First(d => d.Id == labWork.IdDiscipline);
            var userLector = _context.Users.First(u => u.Id == discipline.IdLector);
            var files = _context.Files.ToList();
            var file = files.FirstOrDefault(f => f.IdStudent == user.Id && f.IdLabWork == labWork.Id);
            var score = _context.LabWorkandStudents.ToList().SingleOrDefault(x => x.idlabwork == labWork.Id && x.idstudent == user.Id);
            var model = (
                    labWork,
                    file ??= new FileModel() { Id = 0},
                    user,
                    score ??= new LabWorkandStudent() { idlabwork = 0},
                    userLector);
            return View("~/Views/Discipline/FormsCreate/LabWorkCreate/LabWorkDetails.cshtml", model);
           
        }

        [Authorize(Policy = "lector")]
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
                await
                _context.SaveChangesAsync();
                return RedirectToAction("IndexDiscipline", "Discipline", new { id = labWork.IdDiscipline });
            }
            catch
            {
                return View();
            }
        }

        [Authorize(Policy = "lector")]
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

                labWork = _context.LabWorks.Find(id);
                var idDiscipline = labWork.IdDiscipline;
                _context.LabWorks.Remove(labWork);
                await _context.SaveChangesAsync();
                return RedirectToAction("IndexDiscipline", "Discipline", new { id = idDiscipline });
            }
            catch
            {
                return View();
            }
        }

        [Authorize(Policy = "lector")]
        public ActionResult ConGroupIndex(int id)
        {
            var groups = _context.Group_Disciplines.Where(gd => gd.Iddiscipline == id).ToList();
            var group = _context.Groups.ToList();
            var listIdGroup = new List<int>();

            var listIdGroupDisc = new List<int>();
            foreach (var item in group)
            {
                listIdGroup.Add(item.Id);
            }
            foreach (var item in groups)
            {
                listIdGroupDisc.Add(item.Idgroup);
            }
            List<int> listDontConnGroups = listIdGroup.Except(listIdGroupDisc).ToList();
            var listGroup = new List<Models.Group>();
            foreach (var item in listDontConnGroups)
            {
                listGroup.Add(_context.Groups.First(g => g.Id == item));
            }
            var discipline = _context.Disciplines.First(d => d.Id == id);
            var model = (
            listGroup,
            discipline,
            new Models.Group());
            return View(model);
        }

        [Authorize(Policy = "lector")]
        public async Task<ActionResult> ConnectGroupDiscipline(int idgroup, int iddiscipline)
        {
            var gd = new Group_Discipline()
            {
                Iddiscipline = iddiscipline,
                Idgroup = idgroup
            };

            _context.Group_Disciplines.Add(gd);

            var students = _context.Students.Where(st => st.IdGroup == idgroup);
            foreach (var item in students)
            {
                var score = new Score()
                {
                    userId = item.Id,
                    disciplineId = iddiscipline,
                    score = 0
                };
                _context.Scores.Add(score);
            }
            await _context.SaveChangesAsync();
            return RedirectToAction("ConGroupIndex", "Discipline", new { id = iddiscipline });
        }

        [Authorize(Policy = "lector")]
        public ActionResult DisconGroupIndex(int id)
        {
            var groups = _context.Group_Disciplines.Where(gd => gd.Iddiscipline == id).ToList();

            var listGroup = new List<Models.Group>();
            foreach (var item in groups)
            {
                listGroup.Add(_context.Groups.First(g => g.Id == item.Idgroup));
            }

            var discipline = _context.Disciplines.First(d => d.Id == id);

            var model = (
            listGroup,
            discipline,
            new Models.Group());
            return View(model);
        }

        [Authorize(Policy = "lector")]
        public async Task<ActionResult> DisConnectGroupDiscipline(int idgroup, int iddiscipline)
        {
            var gd = new Group_Discipline()
            {
                Iddiscipline = iddiscipline,
                Idgroup = idgroup
            };
            var students = _context.Students.Where(st => st.IdGroup == idgroup).ToList();
            try
            {
                _context.Group_Disciplines.Remove(gd);
                foreach (var item in students)
                {
                    var score = _context.Scores.First(s => s.userId == item.Id && s.disciplineId == iddiscipline);
                    _context.Scores.Remove(score);
                }
                await _context.SaveChangesAsync();
                return RedirectToAction("DisconGroupIndex", new { id = iddiscipline });
            }
            catch
            {
                return RedirectToAction("DisconGroupIndex", new { id = iddiscipline });
            }

        }

        [HttpGet]
        public ActionResult AddFile(LabWork labWork)
        {
            ClaimsPrincipal claimUser = HttpContext.User;
            var currentUserName = claimUser.Identity.Name;
            var user = _context.Users.First(u => u.Email == currentUserName);
            var model = (
            labWork,
            user.Id,
            _context.Files.ToList());
            return View("~/Views/Discipline/FormsCreate/LabWorkCreate/AddFile.cshtml", model);
        }

        [HttpPost]
        public async Task<ActionResult> AddFile(int idlabwork, int iduser, IFormFile uploadedFile)
        {
            var labwork = _context.LabWorks.Find(idlabwork);
            try
            {
                var file1 = _context.Files.First(x => x.IdLabWork == idlabwork && x.IdStudent == iduser);
                string path = _appEnvironment.WebRootPath + file1.Path;
                if (System.IO.File.Exists(path))
                {
                    System.IO.File.Delete(path);
                }
                _context.Files.Remove(file1);
            }
            catch { }
            int score = 0;
            try
            {
                var scorelab = _context.LabWorkandStudents.First(x => x.idlabwork == idlabwork && x.idstudent == iduser);
                score = scorelab.score;
                _context.LabWorkandStudents.Remove(scorelab);
                _context.SaveChanges();
            }
            catch { }
            if (uploadedFile != null)
            {
                string path = "/Files/" + uploadedFile.FileName;
                using (var fileStream = new FileStream(_appEnvironment.WebRootPath + path, FileMode.Create))
                {
                    await uploadedFile.CopyToAsync(fileStream);
                }
                FileModel file = new FileModel
                {
                    Name = uploadedFile.FileName,
                    Path = path,
                    IdLabWork = idlabwork,
                    IdStudent = iduser
                };
                var labworkandstudent = new LabWorkandStudent()
                {
                    idlabwork = idlabwork,
                    idstudent = iduser,
                    iddiscipline = labwork.IdDiscipline,
                    score = score,
                    passDate = DateTime.Now,
                    idlector = _context.Disciplines.First(d => d.Id == labwork.IdDiscipline).IdLector
                    
                };
                
                _context.Files.Add(file);
                _context.LabWorkandStudents.Add(labworkandstudent);
                _context.SaveChanges();
            }
            return RedirectToAction("AddFile", "Discipline", labwork);
        }

        [Authorize(Policy = "lector")]
        public ActionResult LabWorkStudents(LabWork labWork, int? idgroup)
        {
            var groupsDownList = _context.Groups.ToList();
            ViewBag.Group = groupsDownList.Select(g => g.Title);

            var passedStudent = new List<LabWorkandStudent>();
            var liststudents = new List<UserView>();

            var group = new Models.Group();
            var files = new List<FileModel>();
            try
            {
                group = _context.Groups.First(g => g.Id == idgroup);
                var students = _context.Students.Where(s => s.IdGroup == idgroup).ToList();
                passedStudent = _context.LabWorkandStudents.Where(l => l.idlabwork == labWork.Id).ToList();

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
                passedStudent = _context.LabWorkandStudents.Where(l => l.idlabwork == labWork.Id).ToList();

                foreach (var item in passedStudent)
                {
                    liststudents.Add(_context.Users.First(s => s.Id == item.idstudent));
                }
            }

            if (liststudents.Count != 0)
            {
                foreach (var item in liststudents)
                {
                    files.Add(_context.Files.First(f => f.IdLabWork == labWork.Id && f.IdStudent == item.Id));
                }

            }

            var model = (
                labWork,
                liststudents,
                files,
                passedStudent,
                group);
            return View("~/Views/Discipline/FormsCreate/LabWorkCreate/LabWorkStudents.cshtml", model);
        }

        [HttpPost]
        public async Task<ActionResult> ScoreLabWork(int idlabwork, int iduser, int iddiscipline, int score)
        {
            var labwork = _context.LabWorks.Find(idlabwork);
            ClaimsPrincipal claimUser = HttpContext.User;
            var currentUserName = claimUser.Identity.Name;
            var user = _context.Users.First(u => u.Email == currentUserName);

            var labworkStudent = await _context.LabWorkandStudents.FindAsync(idlabwork, iduser);
            labworkStudent.score = score;
            labworkStudent.idlector = user.Id;
            _context.LabWorkandStudents.Update(labworkStudent);

            await _context.SaveChangesAsync();
            return RedirectToAction("LabWorkStudents", "Discipline", labwork);
        }

        [HttpPost]
        public ActionResult ResultOfLabWorkByGroup(int idlabwork)
        {
            var labwork = _context.LabWorks.Find(idlabwork);
            var groupTitle = Request.Form["group"].ToString();

            if (groupTitle.Length != 0)
            {
                int groupId = _context.Groups.First(g => g.Title == groupTitle).Id;
                return LabWorkStudents(labwork, groupId);
            }
            else
            {
                return LabWorkStudents(labwork, null);
            }
        }

        [Authorize(Policy = "lector")]
        public ActionResult ResultOfDicsipline(int dkey, int? idgroup)
        {
            var groupsDownList = _context.Groups.ToList();
            ViewBag.Group = groupsDownList.Select(g => g.Title);
            var discipline = _context.Disciplines.First(d => d.Id == dkey);
            var scores = new List<Score>();
            var liststudent = new List<UserView>();
            var group = new Models.Group();

            try
            {
                group = _context.Groups.First(g => g.Id == idgroup);
                var students = _context.Students.Where(s => s.IdGroup == idgroup).ToList();
                foreach (var item in students)
                {
                    scores.Add(_context.Scores.First(s => s.userId == item.Id && s.disciplineId == dkey));
                    liststudent.Add(_context.Users.First(u => u.Id == item.Id));
                }
            }
            catch
            {
                scores = _context.Scores.Where(s => s.disciplineId == dkey).ToList();
                foreach (var item in scores)
                {
                    liststudent.Add(_context.Users.First(s => s.Id == item.userId));
                }
            }

            var model = (
                    scores,
                    liststudent,
                    discipline,
                    group);
            return View(model);
        }

        [HttpPost]
        public ActionResult ResultOfDicsiplineByGroup(int iddiscipline)
        {
            var groupTitle = Request.Form["group"].ToString();
            var discipline = _context.Disciplines.First(d => d.Id == iddiscipline);

            if (groupTitle.Length != 0)
            {
                int groupId = _context.Groups.First(g => g.Title == groupTitle).Id;
                return RedirectToAction("ResultOfDicsipline", new { dkey = discipline.Id, idgroup = groupId });
            }
            else
            {
                return RedirectToAction("ResultOfDicsipline", new { dkey = discipline.Id });
            }
        }
            
    }
}