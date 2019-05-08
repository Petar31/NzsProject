using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebApp06.Models;
using WebApp06.Models.Students;
using WebApp06.Models.Test;

namespace WebApp06.Controllers
{
    [Authorize(Roles = "student")]
    public class StudentController : Controller
    {
        private readonly IStudentService studentService;
        private UserManager<ApplicationUser> userManager;
        private readonly ITestService testService;
        public StudentController(IStudentService _studentService, UserManager<ApplicationUser> _userManager, ITestService _testService)
        {
            studentService = _studentService;
            userManager = _userManager;
            testService = _testService;
        }
        public IActionResult Index()
        {
           IEnumerable<SavedTest> savedTest = studentService.GetSavedTests(userManager.GetUserId(User)); 
            return View(savedTest);
        }

        public IActionResult ShowTest(int id)
        {
            List<Question> questions = testService.GetQuestions(id);
            ViewBag.TestId = id;
            string msg = studentService.SaveResult(userManager.GetUserId(User), id, 0);
            ViewBag.Message = msg;
            return View(questions);
        }

        public IActionResult SubmitTest(Dictionary<string, string> keyValuePairs, int testId)
        {
            SolvedTestViewModel solvedTestViewModel = studentService.Result(keyValuePairs);
            string msg = studentService.SaveResult(userManager.GetUserId(User), testId, solvedTestViewModel.Result );
            ViewBag.Message = msg;
            return View(solvedTestViewModel);
        }

        public PartialViewResult _GetTestResults()
        {
            List<TestResultViewModel> tests = studentService.GetTestResults(userManager.GetUserId(User));
            return PartialView(tests);
        }
    }
}