using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp06.Models
{
	public class UserRoleViewModel
	{
		public ApplicationUser ApplicationUser { get; set; }

		public List<IdentityRole> Roles { get; set; }
	}
}
