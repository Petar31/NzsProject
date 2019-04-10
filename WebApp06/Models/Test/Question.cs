using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp06.Models.Test
{

	public class Question
	{
		public int Id { get; set; }

		[Required]
		[StringLength(300)]
		public string Context { get; set; }

		[Required]
		[StringLength(200)]
		public string CorrectAnswer { get; set; }

		public int Grade { get; set; }

		public int Level { get; set; }

		//[Required]
		[StringLength(200)]
		public string WrongAnswer1 { get; set; }

		//[Required]
		[StringLength(200)]
		public string WrongAnswer2 { get; set; }

		//[Required]
		[StringLength(200)]
		public string WrongAnswer3 { get; set; }

		public int SubjectId { get; set; }

		[ForeignKey("SubjectId")]
		public virtual Subject Subject { get; set; }
	}
}
