using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebApp06.Models;
using WebApp06.Models.Test;
using Newtonsoft.Json;
using WebApp06.Models.Students;

namespace WebApp.Controllers
{
	[Authorize(Roles = "professor")]
	public class TestController : Controller
	{
		private readonly ITestService testService;
		private UserManager<ApplicationUser> userManager;
		private RoleManager<IdentityRole> roleManager;

		public TestController(ITestService _testService, UserManager<ApplicationUser> _userManager, RoleManager<IdentityRole> _roleManager)
		{
			testService = _testService;
			userManager = _userManager;
			roleManager = _roleManager;
		}

		[Authorize]
		public IActionResult Index()
		{
            string fName = userManager.Users.Where(x => x.Id == userManager.GetUserId(User)).First().FirstName;
            string lName = userManager.Users.Where(x => x.Id == userManager.GetUserId(User)).First().LastName;
            ViewBag.Name = $"Professor: {fName} {lName}";
            return View();
		}

		public async Task<JsonResult> GetSubjectsAsync(){
			ApplicationUser user = await userManager.GetUserAsync(User);
			List<Subject> subjects = testService.GetSubjects(user.Id);
			return Json(subjects);
		}

		public JsonResult GetGrades(string subjectId){
			List<int> grades = testService.GetGrades(subjectId);
			return Json(grades);
		}

		public JsonResult GetLevels(int grade, int subjectId){
			IEnumerable<int> numOfTests = testService.GetNumOfTestsByGrade(grade, subjectId);
			return Json(numOfTests);
		}

		[HttpPost]
		public JsonResult GenerateTest([FromBody]Post param)
		{
			try
			{

					return Json(testService.GenerateTest(param.Subject, param.Grade, param.Level1, param.Level2, param.Level3));
				
			}
			catch (Exception)
			{

				return Json(new Post());
			}

		}
		[HttpPost]
		public JsonResult SaveTest([FromBody] SaveTestModel	saveTestModel){

            string msg = testService.SaveTest(saveTestModel, userManager.GetUserId(User));
           

			return Json(msg);

		}

	
		public async Task<ActionResult> AddQuestion(string message)
		{
			List<Subject> subjects;
			try
			{
				JsonResult jsonResult = await GetSubjectsAsync();
				 subjects = (List<Subject>)jsonResult.Value;
			}
			catch (Exception)
			{
				subjects = new List<Subject>();
			}
			ViewBag.Message = message;
			ViewBag.Subjects = subjects;
			return View();
		}

		[HttpPost]
		public ActionResult SaveQuestion(Question question){

			string msg = "";
			if (ModelState.IsValid)
			{
				 msg = testService.SaveQuestion(question);
			}
			

			return RedirectToAction("AddQuestion", new { message = msg });
		}



		public IActionResult GetSavedTests(string message = "")
		{
            IEnumerable<SavedTest> savedTests = testService.GetSavedTests(userManager.GetUserId(User));

            ViewBag.Message = message;
            return View(savedTests);
			
		}

        [Route("Test/Details/{id?}/{name?}/{date?}/{grade?}/{group?}")]
        public IActionResult Details(int id, string name, string date, string grade, string group)
        {

            ViewBag.Name = name;
            ViewBag.Date = date;
            ViewBag.Grade = grade;
            ViewBag.Group = group;
            IEnumerable<TestResult> testResults = testService.GetTestResults(id);
            ViewBag.TestResults =  testResults;

            return View(testService.GetQuestions(id));
        }

        [HttpPost]
        public IActionResult DeleteTest(int id)
        {
            string msg = testService.DeleteTest(id);

            return RedirectToAction("GetSavedTests", "Test", new { message = msg});
        }

        public IActionResult GetQuestions()
        {
            ViewBag.Subjects = testService.GetSubjects(userManager.GetUserId(User));
            return View();
        }

        public JsonResult GetQuestionsById(int subjectId, int grade)
        {
            return Json(testService.GetQuestionsById(subjectId, grade));
        }

       


    }
}