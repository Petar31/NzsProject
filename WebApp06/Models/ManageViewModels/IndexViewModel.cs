using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp06.Models.ManageViewModels
{
	public class IndexViewModel
	{
        [Required(ErrorMessage = "First name is required")]
        [StringLength(20, ErrorMessage = "Min 1 characters", MinimumLength = 1)]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last name is required")]
        [StringLength(20, ErrorMessage = "Min 1 characters", MinimumLength = 1)]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "User name is required")]
        [StringLength(30, ErrorMessage = "Min 5 characters", MinimumLength = 5)]
        [Display(Name = "User Name")]
        public string Username { get; set; }

		[Required]
		[EmailAddress]
		public string Email { get; set; }

	

		public string StatusMessage { get; set; }
	}
}
