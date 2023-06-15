using QuizApplication.Models.Role;
using QuizApplication.Models;

namespace QuizApplication.Service.Interface
{
    public interface IRoleService
    {
        BaseResponseModel CreateRole(CreateRoleViewModel request);
        BaseResponseModel DeleteRole(string roleId);
        BaseResponseModel UpdateRole(string roleId, UpdateRoleViewModel request);
        RoleResponseModel GetRole(string roleId);
        RolesResponseModel GetAllRole();
    }
}
