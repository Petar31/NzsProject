using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebApp06.Models;
using WebApp06.Models.Test;

namespace WebApp06.Data
{
	public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
	{
		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
				: base(options)
		{
		}


		protected override void OnModelCreating(ModelBuilder builder)
		{
         
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
          
		}

		public DbSet<Subject> Subjects { get; set; }
		public DbSet<Question> Questions { get; set; }
		public DbSet<SavedTest>	SavedTests { get; set; }
		public DbSet<Professor>	Professors { get; set; }
	}
}
