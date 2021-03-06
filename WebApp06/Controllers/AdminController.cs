﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using WebApp06.Models.Test;
using WebApp06.Models;
using WebApp06.Data;
using Microsoft.EntityFrameworkCore;
using WebApp06.Models.Role;

namespace WebApp06.Controllers
{
	[Authorize(Roles = "admin")]
	public class AdminController : Controller
	{
		private readonly UserManager<ApplicationUser> userManager;
		private readonly RoleManager<IdentityRole> roleManager;
		private readonly ApplicationDbContext dbContext;
		private readonly IRoleService roleService;
		


		public AdminController(UserManager<ApplicationUser> _userManager, RoleManager<IdentityRole> _roleManager, ApplicationDbContext _context, IRoleService _roleService)
		{
			userManager = _userManager;
			roleManager = _roleManager;
			dbContext = _context;
			roleService = _roleService;

		}


		public async Task<IActionResult> Index(string message)
		{
			if (!await IsAdmin())
			{
				return RedirectToAction("Index", "Home");
			}
			ViewBag.Message = message;

			List<UserRoleViewModel> roleViewModels = new List<UserRoleViewModel>();
			foreach (var item in userManager.Users)
			{
				UserRoleViewModel userRoleViewModel = new UserRoleViewModel();
				userRoleViewModel.ApplicationUser = item;
				userRoleViewModel.Roles = dbContext.Roles.FromSql($"select * from AspNetRoles inner join AspNetUserRoles on AspNetRoles.Id = AspNetUserRoles.RoleId where AspNetUserRoles.UserId = {item.Id}").ToList();
				roleViewModels.Add(userRoleViewModel);

			}
			return View(roleViewModels);
		}


		
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> CreateRole(IdentityRole role)
		{
			var res = await roleManager.CreateAsync(role);
			if (res.Succeeded)
			{
				return RedirectToAction("Index", new { message = $"Successfully created role : {role.Name}" });
			}
			return View();
		}


		public PartialViewResult _AddUserInRole()
		{
			ViewBag.Users = dbContext.Users;
			ViewBag.Roles = dbContext.Roles;
			return PartialView();
		}

		[HttpPost]
		public async Task<IActionResult> AddUserInRole(string user, string role)
		{
			ApplicationUser applicationUser = dbContext.Users.Find(user);
			IdentityRole identityRole = dbContext.Roles.Find(role);
			ViewBag.Users = dbContext.Users;
			ViewBag.Roles = dbContext.Roles;
            string msg;
            if (applicationUser != null && identityRole != null)
			{
				bool res = await userManager.IsInRoleAsync(applicationUser, identityRole.Name);
				if (res)
				{
                    msg = $"User {applicationUser.UserName} is already in role {identityRole.Name}";
                    return RedirectToAction("Index", new { message = msg });

                }
				var res1 = await userManager.AddToRoleAsync(applicationUser, identityRole.Name);
				if (res1.Succeeded)
				{
					 msg = $"User {applicationUser.UserName} is added to role {identityRole.Name}";
                    return RedirectToAction("Index", new { message = msg });
                }
				else
				{
					msg = "Error while adding user to role";
                    return RedirectToAction("Index", new { message = msg });

                }
			}
			else
			{
                msg = "User or/and role don't exsists";
                return RedirectToAction("Index", new { message = msg });
            }

           
        }



		private async Task<bool> IsAdmin()
		{
			ApplicationUser user = await userManager.GetUserAsync(User);
			IList<string> roles = await userManager.GetRolesAsync(user);
			bool isRole = roles.Contains("admin");

			return isRole;
		}

		

		private async Task<ApplicationUser> CreateAdmin()
		{
			ApplicationUser admin = await userManager.FindByEmailAsync("admin@admin.com");
			if (admin == null)
			{
				admin = new ApplicationUser
				{
					Email = "admin@admin.com",
					UserName = "admin",


				};
				string password = "Abc123*";
				var res = await userManager.CreateAsync(admin, password);
				if (res.Succeeded)
				{
					return admin;
				}
				else
				{
					return null;
				}

			}
			else
			{
				return admin;
			}
		}

		public async Task<IActionResult> SystemAdmin()
		{

			int roleAdminExists = await roleService.CreateRole("admin");
			int roleProfExsists = await roleService.CreateRole("professor");
			int roleStudentExsists = await roleService.CreateRole("student");

			ApplicationUser admin = await CreateAdmin();

			if (roleAdminExists == -1 || roleProfExsists == -1 || roleStudentExsists == -1)
			{
				ViewBag.Message = "Error while making role";
				return View();
			}
			if (admin == null)
			{
				ViewBag.Message = "Error while making admin";
				return View();
			}
			bool res = await userManager.IsInRoleAsync(admin, "admin");
			if (res)
			{
				ViewBag.Message = "User is already in role admin";
				return View();
			}
			var res1 = await userManager.AddToRoleAsync(admin, "admin");
			if (res1.Succeeded)
			{

				ViewBag.Message = "Succesfully created roles and added user in role admin";
				return View();
			}
			else
			{
				ViewBag.Message = "Error during adding user in role admin";
				return View();
			}


		}

		public PartialViewResult _AddSubject()
		{
			return PartialView();
		}

		[HttpPost]
		public IActionResult AddSubject(Subject subject)
		{
			
			string msg;
			try
			{
			
				dbContext.Subjects.Add(subject);
				dbContext.SaveChanges();
				msg = "Successfuly added subject";
			}
			catch (Exception er)
			{

				msg = er.Message;
			}
			return RedirectToAction("Index", new { message = msg });
		}

		public async Task<PartialViewResult> _AddSubjectToProf(){

			List<Subject> subjects = dbContext.Subjects.ToList();
			IList<ApplicationUser> profs = await userManager.GetUsersInRoleAsync("professor");
			ViewBag.Subjects = subjects;
			ViewBag.Profs = profs;

			return PartialView();
		}

		[HttpPost]
		public ActionResult AddSubjectToProf(string prof, int subject)
		{
			string msg = roleService.AddSubjectToProf(prof, subject);

			return RedirectToAction("Index", new { message = msg });
		}

		public ActionResult _GetRoles()
		{
			try
			{
				List<IdentityRole> roles = dbContext.Roles.ToList();
				return PartialView(roles);
			}
			catch (Exception ex)
			{

				return RedirectToAction("Index", new { message = ex.Message });
			}
			

			
		}

        [HttpPost]
        public ActionResult DeleteUser(string userId)
        {
            string msg = roleService.DeleteUser(userId);
            return RedirectToAction("Index", new { message = msg });
        }


        public PartialViewResult _GetSubjects()
        {
            List<Subject> subjects = dbContext.Subjects.ToList();
            return PartialView(subjects);
        }


		public PartialViewResult _GetProfessors()
		{
			List<ProffesorViewModel> professors = roleService.GetProfessors();
			return PartialView(professors);
		}

        public async Task<ActionResult> DeleteProf(string SubId, string UserId)
        {        
            string msg = await roleService.DeleteProf(SubId, UserId);
            return RedirectToAction("Index", new { message = msg });
        }



        public async Task<ActionResult> DeleteStudent(string id)
        {

            string msg = await roleService.DeleteStudent(id);
            return RedirectToAction("Index", new { message = msg });
        }

        public ActionResult DeleteSubject(int id)
          {
            string msg = roleService.DeleteSubject(id);
            return RedirectToAction("Index", new { message = msg });
          }

        public async Task<PartialViewResult> _ConfigStudents()
        {
            IList<ApplicationUser> students = await userManager.GetUsersInRoleAsync("student");
            ViewBag.Students = students;
            return PartialView();
            
        }

        [HttpPost]
        public ActionResult ConfigStudents(string student, int grade, int group)
        {
            string msg = roleService.ConfigStudents(student, grade, group);
            return RedirectToAction("Index", new { message = msg });
        }

        public PartialViewResult _GetStudents()
        {

            return PartialView(roleService.GetStudents());
        }

    }
}