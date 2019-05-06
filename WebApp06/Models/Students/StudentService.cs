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

        public List<TestResultViewModel> GetTestResults(string userId)
        {
            List<TestResultViewModel> tests;
            try
            {
                tests = (from s in context.Subjects join st in context.SavedTests on s.Id equals st.SubjectId join tr in context.TestResults on st.Id equals tr.TestId where tr.StudentId == userId select new TestResultViewModel { SubjectName = s.Name, TestName = st.Name, TestResult = tr.Result, DateTime = tr.DateTime }).ToList();
            }
            catch (Exception)
            {

                tests = new List<TestResultViewModel>();
            }
            return tests;
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
                int queNum = int.Parse(SubmitedTest.Where(x => x.Key == "queNum").First().Value);
                solvedTestViewModel.Result = Math.Round((solvedTestViewModel.Result / queNum)*100, 2);
            }
            catch (Exception)
            {

                throw;
            }
           
         
            return solvedTestViewModel;
        }

        public string SaveResult(string studentId, int testId, double result)
        {
            try
            {

                TestResult savedTest = context.TestResults.FromSql($"Select * from TestResults where StudentId = {studentId} and TestId = {testId}").FirstOrDefault();
                int save = 0;
                if (savedTest == null)
                {
                    save = context.Database.ExecuteSqlCommand($"insert into TestResults(StudentId, Result, TestId) values({studentId}, {result}, {testId})");
                   
                }
                else
                {
                    save = context.Database.ExecuteSqlCommand($"update TestResults set Result = {result} where StudentId = {studentId} and TestId = {testId}");
                }

                if (save == 0)
                {
                    return "Error";
                }
                else
                {
                    return "Test result saved";
                }


            }
            catch (Exception)
            {

                return "Error";
            }
        }
    }
}
