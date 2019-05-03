using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp06.Models.Test;

namespace WebApp06.Models.Students
{
    public interface IStudentService
    {
        IEnumerable<SavedTest> GetSavedTests(string userId);

        SolvedTestViewModel Result(Dictionary<string, string> SubmitedTest);
    }
}
