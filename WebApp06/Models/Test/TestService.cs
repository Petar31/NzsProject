﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using WebApp06.Models.Test;
using WebApp06.Data;
using WebApp06.Models.Students;

namespace WebApp06.Models.Test
{
    public class TestService : ITestService
    {
        private ApplicationDbContext context;
        public TestService(ApplicationDbContext _context)
        {
            context = _context;

        }

        public List<Question> FindRandomList(List<Question> list, int numQue, int level)
        {
            List<Question> questions = new List<Question>();
            list = list.Where(x => x.Level == level).ToList();

            if (numQue == list.Count())
            {
                return list;
            }
            else
            {
                while (numQue > 0)
                {
                    Random random = new Random();
                    int x = random.Next(0, list.Count());
                    questions.Add(list[x]);
                    list.Remove(list[x]);
                    numQue--;

                }
                return questions;
            }
        }

        public List<Question> GenerateTest(int subject, int grade, int level1 = 0, int level2 = 0, int level3 = 0)
        {

            List<Question> grade4 = GetTestByGrade(grade, subject);
            List<Question> level1List = FindRandomList(grade4, level1, 1);
            List<Question> level2List = FindRandomList(grade4, level2, 2);
            List<Question> level3List = FindRandomList(grade4, level3, 3);
            List<Question> list = level1List.Concat(level2List).Concat(level3List).ToList();

            return list;

        }

        public List<int> GetGrades(string subjectId)
        {
            List<int> grades = context.Questions.FromSql($" select distinct Questions.Grade from Questions where SubjectId = {subjectId}").Select(x => x.Grade).ToList();

            return grades;
        }



        public IEnumerable<int> GetNumOfTestsByGrade(int grade, int subjectId)
        {
            List<Question> grade4 = GetTestByGrade(grade, subjectId);
            int level1 = grade4.Where(x => x.Level == 1).Count();
            int level2 = grade4.Where(x => x.Level == 2).Count();
            int level3 = grade4.Where(x => x.Level == 3).Count();
            IEnumerable<int> numOfTests = new int[] { level1, level2, level3 };
            return numOfTests;
        }



        public List<Subject> GetSubjects(string userId)
        {
            List<Subject> subjects = context.Subjects.FromSql($"select Subjects.Id, Subjects.Name from Subjects join Professors on Subjects.Id = Professors.SubjectId where Professors.UserId = {userId}").ToList();
            return subjects;
        }

        public List<Question> GetTestByGrade(int grade, int subjectId)
        {
            List<Question> questions = context.Questions.FromSql($"Select * from Questions where Grade = {grade} and SubjectId = {subjectId} ").ToList();

            return questions;
        }

        public string SaveQuestion(Question question)
        {
            string message;
            try
            {
                context.Questions.Add(question);
                context.SaveChanges();
                message = "Question added";
            }
            catch (Exception)
            {

                message = "Error";
            }
            return message;

        }

        public string SaveTest(SaveTestModel savedTest, string profId)
        {
            string str = "";
            try
            {

                for (int i = 0; i < savedTest.Ids.Length; i++)
                {
                    str = string.Concat(str, savedTest.Ids[i].ToString(), "|");
                   
                }
                SavedTest test = new SavedTest()
                {
                    Questions = str,
                    Group = savedTest.Group,
                    Name = savedTest.TestName,
                    Date = DateTime.Now,
                    Grade = (from x in context.Questions where x.Id == savedTest.Ids[0] select x.Grade).FirstOrDefault(),
                    ProfessorId = profId,
                    SubjectId = savedTest.SubjectId
                    
                };
                context.SavedTests.Add(test);
                context.SaveChanges();


                return "Successfully saved test";
            }
            catch (Exception ex)
            {

                return ex.Message;
            }


        }

        public IEnumerable<SavedTest> GetSavedTests(string profId)
        {
            IEnumerable<SavedTest> savedTests;
            try
            {
                savedTests = context.SavedTests.Where(x => x.ProfessorId == profId);
                
            }
            catch (Exception)
            {
                savedTests = Enumerable.Empty<SavedTest>();
            }

            return savedTests;
        }

        public List<Question> GetQuestions(int id)
        {
            List<Question> questions = new List<Question>();
            try
            {
                SavedTest savedTest = context.SavedTests.Find(id);
                List<int> questionIds = new List<int>();

                for (int i = 0; i < savedTest.Questions.Split("|").Length; i++)
                {
                    if (savedTest.Questions.Split("|")[i] != "")
                    {
                         questionIds.Add(int.Parse(savedTest.Questions.Split("|")[i]));
                    }
                   
                }

                foreach (int item in questionIds)
                {
                    questions.Add(context.Questions.Where(x => x.Id == item).SingleOrDefault());
                }

                return questions;
            }
            catch (Exception)
            {

                return questions;
            }

           
        }

        public string DeleteTest(int id)
        {
           
            try
            {

                //context.SavedTests.FromSql($"Delete from SavedTests where Id = {id}");
                SavedTest savedTest = context.SavedTests.Find(id);
                context.SavedTests.Remove(savedTest);
                context.SaveChanges();
                return "Test deleted";
               
            }
            catch (Exception ex)
            {

                return ex.Message;
            }
        }

       
        public IEnumerable<Question> GetQuestionsById(int subjectId, int grade)
        {
            IEnumerable<Question> questions;
            try
            {
                questions = context.Questions.FromSql($"Select * from Questions where SubjectId ={subjectId} and Grade = {grade}");
            }
            catch (Exception)
            {

                questions = Enumerable.Empty<Question>();
            }
            return questions;
        }

        public IEnumerable<TestResult> GetTestResults(int testId)
        {
            IEnumerable<TestResult> testResults;
            try
            {
                testResults = (from tr in context.TestResults where tr.TestId == testId select tr).Include(x=>x.ApplicationUser);
            }
            catch (Exception)
            {

                testResults = Enumerable.Empty<TestResult>();
            }
            return testResults;
        }

        public string DeleteQuestions(int[] ids)
        {
            string msg = "";
            try
            {
                foreach (var id in ids)
                {
                    int x = context.Database.ExecuteSqlCommand($"Delete from Questions where Id = {id}");
                    if (x > 0)
                    {
                        msg = "Questions deleted";
                    }
                    else
                    {
                        msg = "Error";
                    }
                }
              
            }
            catch (Exception)
            {

                msg = "Error";
            }
            return msg;
        }
    }
}
