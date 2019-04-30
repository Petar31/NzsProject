using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace WebApp06.Controllers
{
    public class SudentController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}