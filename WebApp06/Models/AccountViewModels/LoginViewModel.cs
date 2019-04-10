using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp06.Models.AccountViewModels
{
	public class LoginViewModel
	{
		[Required(ErrorMessage = "User name is required")]
		[StringLength(30, ErrorMessage = "Min 5 characters", MinimumLength = 5)]
		[Display(Name = "User Name")]
		public string UserName { get; set; }

		[Required]
		[DataType(DataType.Password)]
		public string Password { get; set; }

		[Display(Name = "Remember me?")]
		public bool RememberMe { get; set; }
	}
}
