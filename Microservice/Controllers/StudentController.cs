using Microsoft.AspNetCore.Mvc;
using Microservice.Models;

namespace Microservice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : Controller
    {
        private ApplicationContext db = new ApplicationContext();
        [HttpGet]
        public IActionResult StudentView()
        {
            ViewBag.Students = db.Students.ToList();
            return View();
        }
    }
}
