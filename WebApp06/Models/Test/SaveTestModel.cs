using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp06.Models.Test
{
	public class SaveTestModel
	{
        public string TestName { get; set; }
        public int Group { get; set; }
        public int[] Ids { get; set; }

        public int SubjectId { get; set; }
    }
}
