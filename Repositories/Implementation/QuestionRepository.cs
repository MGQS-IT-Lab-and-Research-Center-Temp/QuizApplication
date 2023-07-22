using Microsoft.EntityFrameworkCore;
using QuizApplication.Context;
using QuizApplication.Entities;
using QuizApplication.Repositories.Interface;
using System.Linq.Expressions;

namespace QuizApplication.Repositories.Implementation
{
    public class QuestionRepository : BaseRepository<Question>, IQuestionRepository
    {
        public QuestionRepository(QuizApplicationContext context) : base(context)
        {
        }

        public Question GetQuestion(Expression<Func<Question, bool>> expression)
        {
            var question = _context.Questions
                .Include(c => c.User)
                .SingleOrDefault(expression);

            return question;
        }

        public List<Question> GetQuestions()
        {
            var questions = _context.Questions
                .Include(uq => uq.User)
                .ToList();

            return questions;
        }

        public List<Question> GetQuestions(Expression<Func<Question, bool>> expression)
        {
            var questions = _context.Questions
                .Where(expression)
                .Include(u => u.User)
                .ToList();

            return questions;
        }

        public List<Question> GetQuestionBySubjectId(string subjectId)
        {
            var questions = _context.Questions
                .Include(c => c.Subject)
                .Where(c => c.SubjectId.Equals(subjectId))
                .ToList();

            return questions;
        }

        public List<Question> SelectQuestionBySubject()
        {
            var questions = _context.Questions
                .Include(c => c.Subject)
                .ToList();

            return questions;
        }
    }
}
