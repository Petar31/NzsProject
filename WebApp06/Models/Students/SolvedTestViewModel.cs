using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp06.Models.Students
{
    public class SolvedTestViewModel
    {
        public Dictionary<string, string> SolvedTest { get; set; }

        public int Result { get; set; }
    }
}
