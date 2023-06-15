using QuizApplication.Models;
using QuizApplication.Models.Auth;
using QuizApplication.Models.User;

namespace QuizApplication.Service.Interface
{
    public interface IUserService
    {
        UserResponseModel GetUser(string userId);
        BaseResponseModel Register(SignUpViewModel request, string roleName = null);
        UserResponseModel Login(LoginViewModel request);
    }
}
