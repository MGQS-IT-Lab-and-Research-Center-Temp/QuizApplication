﻿using QuizApplication.Entities;
using QuizApplication.Helper;
using QuizApplication.Models;
using QuizApplication.Models.Auth;
using QuizApplication.Models.User;
using QuizApplication.Repositories.Interface;
using QuizApplication.Service.Interface;

namespace QuizApplication.Service.Implementation
{
    public class UserService : IUserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUnitOfWork _unitOfWork;

        public UserService(IHttpContextAccessor httpContextAccessor, IUnitOfWork unitOfWork)
        {
            _httpContextAccessor = httpContextAccessor;
            _unitOfWork = unitOfWork;
        }

        public BaseResponseModel Register(SignUpViewModel request, string className)
        {
            var response = new BaseResponseModel();
            string saltString = HashingHelper.GenerateSalt();
            string hashedPassword = HashingHelper.HashPassword(request.Password, saltString);
            var createdBy = _httpContextAccessor.HttpContext.User.Identity.Name;
            var userExist = _unitOfWork.Users.Exists(x => x.UserName == request.UserName || x.Email == request.Email);

            if (userExist)
            {
                response.Message = $"User with {request.UserName} or {request.Email} already exist";
                return response;
            }

            className ??= "AppUser";

            var role = _unitOfWork.Roles.Get(x => x.ClassName == className);

            if (role is null)
            {
                response.Message = $"Role does not exist";
                return response;
            }

            var user = new User
            {
                UserName = request.UserName,
                Email = request.Email,
                HashSalt = saltString,
                PasswordHash = hashedPassword,
                RoleId = role.Id,
                CreatedBy = createdBy,
            };

            try
            {
                _unitOfWork.Users.Create(user);
                _unitOfWork.SaveChanges();
                response.Message = $"You have succesfully signed up on This Plat Form ";
                response.Status = true;

                return response;
            }
            catch (Exception ex)
            {
                return new BaseResponseModel
                {
                    Message = $"Unable to signup, an error occurred {ex.Message}"
                };
            }
        }

        public UserResponseModel GetUser(string userId)
        {
            var response = new UserResponseModel();
            var user = _unitOfWork.Users.GetUser(x => x.Id == userId);

            if (user is null)
            {
                response.Message = $"User with {userId} does not exist";
                return response;
            }

            response.Data = new UserViewModel
            {
                UserName = user.UserName,
                Email = user.Email,
                RoleId = user.RoleId,
                RoleName = user.Role.ClassName,
            };
            response.Message = $"User successfully retrieved";
            response.Status = true;

            return response;
        }

        public UserResponseModel Login(LoginViewModel model)
        {
            var response = new UserResponseModel();

            try
            {
                var user = _unitOfWork.Users.GetUser(x =>
                                (x.UserName.ToLower() == model.UserName.ToLower()
                                || x.Email.ToLower() == model.UserName.ToLower()));

                if (user is null)
                {
                    response.Message = $"Account does not exist!";
                    return response;
                }

                string hashedPassword = HashingHelper.HashPassword(model.Password, user.HashSalt);

                if (!user.PasswordHash.Equals(hashedPassword))
                {
                    response.Message = $"Incorrect username or password!";
                    return response;
                }

                response.Data = new UserViewModel
                {
                    Id = user.Id,
                    UserName = user.UserName,
                    Email = user.Email,
                    RoleId = user.RoleId,
                    RoleName = user.Role.ClassName,
                };
                response.Message = $"You Are Welcome {user.UserName}";
                response.Status = true;

                return response;
            }
            catch (Exception ex)
            {
                response.Message = $"An error occured: {ex.Message}";
                return response;
            }
        }
    }
}
