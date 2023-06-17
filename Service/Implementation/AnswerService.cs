using QuizApplication.Entities;
using QuizApplication.Models;
using QuizApplication.Models.Answer;
using QuizApplication.Repositories.Interface;
using QuizApplication.Service.Interface;
using System.Security.Claims;

namespace QuizApplication.Service.Implementation
{ 
    public class AnswerService : IAnswerService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AnswerService(
            IUnitOfWork unitOfWork,
            IHttpContextAccessor httpContextAccessor)
        {
            _unitOfWork = unitOfWork;
            _httpContextAccessor = httpContextAccessor;
        }

        public BaseResponseModel WriteAnswer(CreateAnswerViewModel request)
        {
            var response = new BaseResponseModel();
            var createdBy = _httpContextAccessor.HttpContext.User.Identity.Name;
            var userIdClaim = _httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
            var user = _unitOfWork.Users.Get(userIdClaim);

            if (user is null)
            {
                response.Message = "User not found";
                return response;
            }

            var question = _unitOfWork.Questions.Get(request.QuestionId);

            if (question is null)
            {
                response.Message = "Question not found";
                return response;
            }

            if (string.IsNullOrWhiteSpace(request.AnswerChoosed))
            {
                response.Message = "Answer is required!";
                return response;
            }

            var answer = new Answer
            {
                UserId = user.Id,
                User = user,
                QuestionId = question.Id,
                Question = question,
                AnswerText = request.AnswerChoosed,
                CreatedBy = createdBy,
            };

            try
            {
                _unitOfWork.Answers.Create(answer);
                _unitOfWork.SaveChanges();
                response.Status = true;
                response.Message = "";

                return response;
            }
            catch (Exception ex)
            {
                response.Message = $"An Answer is Required.{ex.Message}";
                return response;
            }
        }

        public BaseResponseModel ChangeAnswer(string answerId, UpdateAnswerViewModel request)
        {
            var response = new BaseResponseModel();
            var modifiedBy = _httpContextAccessor.HttpContext.User.Identity.Name;
            var answerexist = _unitOfWork.Answers.Exists(c => c.Id == answerId);
            var userIdClaim = _httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
            var user = _unitOfWork.Users.Get(userIdClaim);

            if (!answerexist)
            {
                response.Message = "Answer  does not exist.";
                return response;
            }
            var answer = _unitOfWork.Answers.Get(answerId);

            if (answer.UserId != userIdClaim)
            {
                response.Message = "You can not change your answer";
                return response;
            }

            answer.AnswerText = request.AnswerChoosed;
            answer.ModifiedBy = modifiedBy;

            try
            {
                _unitOfWork.Answers.Update(answer);
                _unitOfWork.SaveChanges();
            }
            catch (Exception ex)
            {
                response.Message = $"Could not change your answer : {ex.Message}";
                return response;
            }
            response.Status = true;
            response.Message = "Answer Changed successfully.";

            return response;
        }
    }

}