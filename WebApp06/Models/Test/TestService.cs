using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using WebApp06.Models.Test;
using WebApp06.Data;

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

		public string SaveTest(SaveTestModel savedTest)
		{
			try
			{
				string str = "";
				try
				{

					for (int i = 0; i < savedTest.Ids.Length; i++)
					{
						str = string.Concat(savedTest.Ids[i].ToString(), "|");
					}
					SavedTest test = new SavedTest()
					{
						Questions = str
					};
					context.SavedTests.Add(test);
					context.SaveChanges();

				}
				catch (Exception)
				{

					throw;
				}


		
				return "Successfully saved test";
			}
			catch (Exception ex)
			{

				return ex.Message;
			}
		}
	}
}
