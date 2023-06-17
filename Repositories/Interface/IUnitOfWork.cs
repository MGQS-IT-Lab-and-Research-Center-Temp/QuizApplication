namespace QuizApplication.Repositories.Interface
{
    public interface IUnitOfWork : IDisposable
    {
        IRoleRepository Roles { get; }
        IUserRepository Users { get; }
        ISubjectRepository Subjects { get; }
        IQuestionRepository Questions { get; }
        IAnswerRepository Answers { get; }
        int SaveChanges();
    }
}