using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp06.Data;
using WebApp06.Models.Test;

namespace WebApp06.Models.Role
{
	public class RoleService : IRoleService
	{
		private ApplicationDbContext context;
		private UserManager<ApplicationUser> userManager;

		public RoleService(ApplicationDbContext _context, UserManager<ApplicationUser> _userManager)
		{
			context = _context;
			userManager = _userManager;
		}

		public string AddSubjectToProf(string prof, int subject)
		{
			string message;
			try
			{
				context.Database.ExecuteSqlCommand($"Insert into Professors values ({subject}, {prof})");
				message = "Successfully added professor";
			}
			catch (Exception ex)
			{
				message = ex.Message;
			}
			return message;
		}

		public List<Professor> GetProfessors()
		{

			List<Professor> professors = context.Professors.Include(x => x.ApplicationUser).Include(x => x.Subject).ToList();

			return professors;
		}
	}
}
