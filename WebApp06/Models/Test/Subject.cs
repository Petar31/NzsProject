using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApp06.Models.Test
{
	public class Subject
	{
		[Key]
		public int Id { get; set; }

		[Required(ErrorMessage ="Subject name is required")]
		[StringLength(30)]
		public string Name { get; set; }

        [NotMapped]
        [InverseProperty("Subject")]
        public List<Professor> Professors { get; set; }
    }
}
