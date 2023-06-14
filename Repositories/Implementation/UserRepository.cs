using Microsoft.EntityFrameworkCore;
using QuizApplication.Context;
using QuizApplication.Entities;
using QuizApplication.Repositories.Interface;
using System.Linq.Expressions;

namespace QuizApplication.Repositories.Implementation
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(QuizApplicationContext context) : base(context)
        {
        }

        public User GetUser(Expression<Func<User, bool>> expression)
        {
            return _context.Users
                .Include(x => x.Role)
                .SingleOrDefault(expression);
        }
    }
}
