using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp06.Models.Test
{
	public class SavedTest
	{
		[Key]
		public int Id { get; set; }

		public string Questions { get; set; }

		public DateTime Date { get; set; }
	}
}
