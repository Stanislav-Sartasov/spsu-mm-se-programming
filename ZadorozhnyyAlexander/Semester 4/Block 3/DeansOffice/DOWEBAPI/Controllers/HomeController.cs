﻿using DOWEBAPI.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using DeansOffice;

namespace DOWEBAPI.Controllers
{
    public class HomeController : Controller
    {
        private static IExamSystem _examSystem = new ExamSystemS();

        [Route("/")]
        public IActionResult Index() => View(_examSystem.Count);

        public IActionResult Add() => View();

        [HttpPost]
        public IActionResult Add(Exam exam)
        {
            if (ModelState.IsValid)
                _examSystem.Add(exam.studentId, exam.courseId);
            return Redirect("/");
        }

        public IActionResult Remove() => View();

        [HttpPost]
        public IActionResult Remove(Exam exam)
        {
            if (ModelState.IsValid)
                _examSystem.Remove(exam.studentId, exam.courseId);

            return Redirect("/");
        }

        public IActionResult Contains() => View();

        [HttpPost]
        public IActionResult Contains(Exam exam)
        {
            if (ModelState.IsValid)
            {
                if (_examSystem.Contains(exam.studentId, exam.courseId))
                    return View("Confirm");
                else
                    return View("Negative");
            }

            return Redirect("/");
        }

        public IActionResult Confirm() => View();

        public IActionResult Negative() => View();

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}