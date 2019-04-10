using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp06.Models.Test
{

	public class Professor
	{
		public int SubjectId { get; set; }
		[Key]
		public string UserId { get; set; }



		[ForeignKey("SubjectId")]
		public virtual Subject Subject { get; set; }

		[ForeignKey("UserId")]
		public virtual ApplicationUser ApplicationUser { get; set; }
	}
}
