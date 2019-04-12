using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApp06.Data;
using WebApp06.Models;

namespace WebApp06.Controllers
{
	public class HomeController : Controller
	{
		private ApplicationDbContext dbContext;

		public HomeController(ApplicationDbContext _dbContext)
		{
			dbContext = _dbContext;
		}

		public IActionResult Index()
		{
			bool adminExsists = false;
			if (dbContext.Roles.Where(x => x.Name == "admin").Count() != 0)
			{
				adminExsists = true;
			}
            if (User.IsInRole("admin"))
            {
                return RedirectToAction("Index", "Role");
            }

			//bool adminExsists = true;
			ViewBag.AdminExsists = adminExsists;
			return View();
		}

		public IActionResult About()
		{
			ViewData["Message"] = "Your application description page.";

			return View();
		}

		public IActionResult Contact()
		{
			ViewData["Message"] = "Your contact page.";

			return View();
		}

		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}
