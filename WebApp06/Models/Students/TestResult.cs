using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using WebApp06.Models.Test;

namespace WebApp06.Models.Students
{
    public class TestResult
    {
        [Key]
        public string StudentId { get; set; }

        public int TestId { get; set; }

        public DateTime DateTime { get; set; }

        public double Result { get; set; }

        [ForeignKey("StudentId")]
        public virtual ApplicationUser ApplicationUser { get; set; }

        [ForeignKey("TestId")]
        public virtual SavedTest SavedTest { get; set; }
    }
}
