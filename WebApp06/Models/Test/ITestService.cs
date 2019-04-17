using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp06.Models.Test
{
	public interface ITestService
	{
		List<int> GetGrades(string subject);

		List<Subject> GetSubjects(string userId);

		List<Question> GetTestByGrade(int grade, int subjectId);

		List<Question> GenerateTest(int subject, int grade, int level1, int level2, int level3);

		List<Question> FindRandomList(List<Question> list, int numQue, int level);

		IEnumerable<int> GetNumOfTestsByGrade(int grade, int subjectId);

		string SaveTest(SaveTestModel savedTest, string profId);

		string SaveQuestion(Question question);

        IEnumerable<SavedTest> GetSavedTests(string profId);

        List<Question> GetQuestions(int id);

        string DeleteTest(int id);

        IEnumerable<Question> GetQuestionsById(int subjectId, int grade);



    }
}
