using QuizApplication.Entities;
using System.Linq.Expressions;

namespace QuizApplication.Repositories.Interface
{
    public interface IQuestionRepository : IRepository<Question>
    {
        List<Question> GetQuestions();
        List<Question> GetQuestions(Expression<Func<Question, bool>> expression);
        Question GetQuestion(Expression<Func<Question, bool>> expression);
        List<SubjectQuestion> GetQuestionBySubjectId(string id);
        List<SubjectQuestion> SelectQuestionBySubject();
    }
}
