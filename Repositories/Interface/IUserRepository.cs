using QuizApplication.Entities;
using System.Linq.Expressions;

namespace QuizApplication.Repositories.Interface
{
    public interface IUserRepository : IRepository<User>
    {
        User GetUser(Expression<Func<User, bool>> expression);
    }

}
