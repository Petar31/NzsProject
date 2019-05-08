using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp06.Models.Test;

namespace WebApp06.Models.Role
{
    public interface IRoleService
    {
        string AddSubjectToProf(string prof, int subject);

        List<ProffesorViewModel> GetProfessors();

        Task<string> DeleteProf(string SubId, string UserId);

        Task<string> DeleteStudent(string UserId);

        string DeleteSubject(int subjectId);

        string ConfigStudents(string id, int grade, int group);

        IEnumerable<Student> GetStudents();

        string DeleteUser(string userId);

    }
}
