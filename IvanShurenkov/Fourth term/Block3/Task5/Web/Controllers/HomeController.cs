using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Reflection;
using Web.Models;
using Task5.ExamSystem;

namespace Web.Controllers
{
    public class HomeController : Controller
    {
        private static IExamSystem _examSystem = new ExamHashTable(32);

        public async Task<IActionResult> Index()
        {
            var model = new ExamModel();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Add(ExamModel model)
        {
            var rnd = new Random();
            model.StudentId = rnd.Next();
            model.CourseId = rnd.Next();

            _examSystem.Add(model.StudentId, model.CourseId);
            model.LableText = string.Empty;
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Remove(ExamModel model)
        {
            var rnd = new Random();
            model.StudentId = rnd.Next();
            model.CourseId = rnd.Next();

            _examSystem.Remove(model.StudentId, model.CourseId);
            model.LableText = string.Empty;
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Contains(ExamModel model)
        {
            var rnd = new Random();
            model.StudentId = rnd.Next();
            model.CourseId = rnd.Next();

            model.LableText = String.Format("{0}(Student Id), {1}(Course Id) - {2}",
                model.StudentId, model.CourseId, _examSystem.Contains(model.StudentId, model.CourseId));
            return View("Index", model);
        }

        [HttpPost]
        public async Task<IActionResult> Count(ExamModel model)
        {
            model.LableText = String.Format("Count = {0}", _examSystem.Count);
            return View("Index", model);
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}