using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp06.Data;
using WebApp06.Models.Test;

namespace WebApp06.Models.Students
{
    public class StudentService : IStudentService
    {
        private ApplicationDbContext context;

        public StudentService(ApplicationDbContext _context)
        {
            context = _context;
        }
        public IEnumerable<SavedTest> GetSavedTests(string userId)
        {
            IEnumerable<SavedTest> savedTests;
            try
            {
                Student student = context.Students.FromSql($"Select * from Students where UserId = {userId}").FirstOrDefault();
                //savedTests = context.SavedTests.FromSql($"Select * from SavedTests where [Grade] = {student.Grade} and [Group] = {student.Group}").ToList();
                savedTests = context.SavedTests.Include(x => x.Subject).Where(y => y.Grade == student.Grade && y.Group == student.Group).ToList();

            }
            catch (Exception)
            {

                savedTests = Enumerable.Empty<SavedTest>();
            }

            return savedTests;
        }

        public SolvedTestViewModel Result(Dictionary<string, string> SubmitedTest)
        {
            SolvedTestViewModel solvedTestViewModel = new SolvedTestViewModel();
            try
            {
                solvedTestViewModel.Result = 0;
                solvedTestViewModel.SolvedTest = new Dictionary<string, string>();
                foreach (var item in SubmitedTest)
                {

                    bool boolId = int.TryParse(item.Key, out int id);
                    if (boolId)
                    {
                        Question question = context.Questions.FromSql($"Select * from Questions where Id = {id}").FirstOrDefault();
                        if (item.Value == question.CorrectAnswer)
                        {
                            solvedTestViewModel.Result++;

                            solvedTestViewModel.SolvedTest.Add(question.Context, "true");
                        }
                        else
                        {
                            solvedTestViewModel.SolvedTest.Add(question.Context, "false");
                        }
                    }

                }
            }
            catch (Exception)
            {

                throw;
            }
           
         
            return solvedTestViewModel;
        }
    }
}
