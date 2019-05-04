using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WebApp06.Models.Test;

namespace WebApp06.Models
{
	// Add profile data for application users by adding properties to the ApplicationUser class
	public class ApplicationUser : IdentityUser
    {
        public ApplicationUser()
        {
            Professors = new HashSet<Professor>();
        }

		[StringLength(20)]
		public string FirstName { get; set; }

		[StringLength(20)]
		public string LastName { get; set; }

        
        [InverseProperty("ApplicationUser")]
        public ICollection<Professor> Professors { get; set; }




    }
}
