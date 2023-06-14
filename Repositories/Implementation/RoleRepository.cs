using QuizApplication.Context;
using QuizApplication.Entities;
using QuizApplication.Repositories.Interface;

namespace QuizApplication.Repositories.Implementation
{
    public class RoleRepository : BaseRepository<Role>, IRoleRepository
    {
        public RoleRepository( QuizApplicationContext context) : base(context)
        {
        }
    }
}
  