using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp06.Data;
using WebApp06.Models.Test;

namespace WebApp06.Models.Role
{
    public class RoleService : IRoleService
    {
        private ApplicationDbContext context;
        private UserManager<ApplicationUser> userManager;

        public RoleService(ApplicationDbContext _context, UserManager<ApplicationUser> _userManager)
        {
            context = _context;
            userManager = _userManager;
        }

        public string AddSubjectToProf(string prof, int subject)
        {
            string message;
            try
            {
                context.Database.ExecuteSqlCommand($"Insert into Professors values ({subject}, {prof})");
                message = "Successfully added professor";
            }
            catch (Exception ex)
            {
                message = ex.Message;
            }
            return message;
        }

        public List<Professor> GetProfessors()
        {

            List<Professor> professors = context.Professors.Include(x => x.ApplicationUser).Include(x => x.Subject).ToList();
          

            return professors;
        }

        public string DeleteProf(string SubId, string UserId)
        {
            try
            {
                int subId = int.Parse(SubId);
                Professor professor = context.Professors.Where(x => x.SubjectId == subId && x.UserId == UserId).SingleOrDefault();

                context.Professors.Remove(professor);
                context.SaveChanges();
                return "User is no more prof for the subject";
               
            }
            catch (Exception)
            {

                return "Error";
            }
        }

        public string ConfigStudents(string id, int grade, int group)
        {
            try
            {

                context.Database.ExecuteSqlCommand($"exec InsertIntoStudents @id = {id}, @grade = {grade}, @group = {group};");
                return "Student updated";
            }
            catch (Exception)
            {

                return "Error";
            }
        }

        public IEnumerable<Student> GetStudents()
        {
            IEnumerable<Student> students;
            try
            {
                students = context.Students.Include(x=>x.ApplicationUser).ToList();
            }
            catch (Exception)
            {
                students = Enumerable.Empty<Student>();
                
            }
            return students;
        }
    }
}
