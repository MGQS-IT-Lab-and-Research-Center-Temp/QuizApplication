using QuizApplication.Context;
using QuizApplication.Entities;
using QuizApplication.Models.Subject;
using QuizApplication.Repositories.Interface;

namespace QuizApplication.Repositories.Implementation;

public class SubjectRepository : BaseRepository<Subject>, ISubjectRepository
{
    public SubjectRepository(QuizApplicationContext context) : base(context)
    {
    }

    
}