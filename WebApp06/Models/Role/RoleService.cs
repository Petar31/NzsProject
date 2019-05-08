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
        private readonly RoleManager<IdentityRole> roleManager;
        public RoleService(ApplicationDbContext _context, UserManager<ApplicationUser> _userManager, RoleManager<IdentityRole> _roleManager)
        {
            context = _context;
            userManager = _userManager;
            roleManager = _roleManager;
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

        public List<ProffesorViewModel> GetProfessors()
        {
            List<ProffesorViewModel> proffesorViewModels = new List<ProffesorViewModel>();
            List<Professor> professors = context.Professors.FromSql("Select * from [Professors]").ToList();
            foreach (var item in professors)
            {
                ProffesorViewModel proffesorViewModel = new ProffesorViewModel();
                ApplicationUser user = context.Users.Find(item.UserId);
                proffesorViewModel.FirstName = user.FirstName;
                proffesorViewModel.LastName = user.LastName;
                proffesorViewModel.UserName = user.UserName;
                proffesorViewModel.Subject = context.Subjects.Find(item.SubjectId).Name;
                proffesorViewModel.SubjectId = item.SubjectId;
                proffesorViewModel.UserId = item.UserId;

                proffesorViewModels.Add(proffesorViewModel);
            }
          

            return proffesorViewModels;
        }

        public async Task<string> DeleteProf(string SubId, string UserId)
        {
            try
            {
                ApplicationUser applicationUser = context.Users.Find(UserId);
                List<Professor> professors = context.Professors.FromSql($"Select * from Professors where UserId = {UserId}").ToList();
                if (professors.Count == 1)
                {
                  var result =  await userManager.RemoveFromRoleAsync(applicationUser, "professor");
                    if (result.Succeeded)
                    {
                        return "User deleted from the role";
                    }
                    else
                    {
                        return "User cannot be deleted from the role";
                    }
                }

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

        public string DeleteUser(string userId)
        {
            try
            {
                int x = context.Database.ExecuteSqlCommand($"Delete from AspNetUsers where Id = {userId}");
                if (x != 0)
                {
                    return "User deleted";
                }
                else
                {
                    return "Error";
                }
            }
            catch (Exception)
            {

                return "Error";
            }
        }

        public async Task<string> DeleteStudent(string UserId)
        {
            try
            {
                ApplicationUser applicationUser = context.Users.Find(UserId);
                var result = await userManager.RemoveFromRoleAsync(applicationUser, "student");
                if (result.Succeeded)
                {
                    int x = context.Database.ExecuteSqlCommand($"Delete from Students where UserId = {UserId}");
                    if (x == 1)
                    {
                        return "Student deleted";
                    }
                    else
                    {
                        return "Error";
                    }
                }
                else
                {
                    return "User cannot be deleted from the role";
                }
               
            }
            catch (Exception)
            {

                return "Error";
            }
        }

        public string DeleteSubject(int subjectId)
        {
            try
            {
                int x = context.Database.ExecuteSqlCommand($"Delete from Subjects where Id = {subjectId}");
                if (x == 1)
                {
                    return "Subject deleted";
                }
                else
                {
                    return "Error";
                }
            }
            catch (Exception)
            {

                return "Error";
            }
        }

      

        public async Task<int> CreateRole(string role)
        {
            bool roleExists = await roleManager.RoleExistsAsync(role);
            if (roleExists)
            {
                return 0;
            }
            else
            {
                IdentityRole identityRole = new IdentityRole(role);
                var result = await roleManager.CreateAsync(identityRole);
                if (result.Succeeded)
                {
                    return 1;
                }
                else
                {
                    return -1;
                }
            }


        }

       
    }
}
