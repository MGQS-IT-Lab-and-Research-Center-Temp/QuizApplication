using QuizApplication.Context;
using QuizApplication.Repositories.Interface;

namespace QuizApplication.Repositories.Implementation
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly QuizApplicationContext _context;
        private bool _disposed = false;
        public IRoleRepository Roles { get; }
        public IUserRepository Users { get; }
        public ISubjectRepository Subjects { get; }
        public IQuestionRepository Questions { get; }

        public UnitOfWork(
            QuizApplicationContext context,
            IRoleRepository roleRepository,
            IUserRepository userRepository,
            ISubjectRepository subjectRepository,
            IQuestionRepository questionRepository)
            
        {
            _context = context;
            Roles = roleRepository;
            Users = userRepository;
            Subjects = subjectRepository;
            Questions = questionRepository;
        }

        public int SaveChanges()
        {
            return _context.SaveChanges();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
                _disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
