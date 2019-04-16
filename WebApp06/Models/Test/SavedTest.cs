using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp06.Models.Test
{
	public class SavedTest
	{
		[Key]
		public int Id { get; set; }

		public string Questions { get; set; }

        [StringLength(40)]
        public string Name { get; set; }

        public DateTime Date { get; set; }

        public int Grade { get; set; }

        public int Group { get; set; }

        public string ProfessorId { get; set; }

        [ForeignKey("ProfessorId")]
        public virtual ApplicationUser ApplicationUser { get; set; }
    }
}
