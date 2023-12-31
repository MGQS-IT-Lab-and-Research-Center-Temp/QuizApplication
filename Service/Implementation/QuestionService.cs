﻿using QuizApplication.Entities;
using QuizApplication.Models;
using QuizApplication.Models.Question;
using QuizApplication.Repositories.Interface;
using QuizApplication.Service.Interface;
using System.Linq.Expressions;
using System.Security.Claims;

namespace QuizApplication.Service.Implementation
{
    public class QuestionService : IQuestionService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUnitOfWork _unitOfWork;

        public QuestionService(
            IHttpContextAccessor httpContextAccessor,
            IUnitOfWork unitOfWork)
        {
            _httpContextAccessor = httpContextAccessor;
            _unitOfWork = unitOfWork;
        }

        public BaseResponseModel Create(CreateQuestionViewModel request)
        {
            var response = new BaseResponseModel();
            var createdBy = _httpContextAccessor.HttpContext.User.Identity.Name;
            var userIdClaim = _httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
            var user = _unitOfWork.Users.Get(userIdClaim);
            var question = new Question
            {
                UserId = user.Id,
                QuestionText = request.QuestionText,
                CreatedBy = createdBy,
            };

            var subjects = _unitOfWork.Subjects.GetAllByIds(request.SubjectIds);

            var subjectQuestions = new HashSet<Question>();

            foreach (var subject in subjects)
            {
                var subjectQuestion = new Question
                {
                    SubjectId = subject.Id,
                    Subject = subject,
      
                };

                subjectQuestions.Add(subjectQuestion);
            }


            try
            {
                _unitOfWork.Questions.Create(question);
                _unitOfWork.SaveChanges();
                response.Message = "Question created successfully!";
                response.Status = true;

                return response;
            }
            catch (Exception ex)
            {
                response.Message = $"Failed to create question: {ex.Message}";
                return response;
            }
        }

        public BaseResponseModel Update(string questionId, UpdateQuestionViewModel request)
        {
            var response = new BaseResponseModel();
            var modifiedBy = _httpContextAccessor.HttpContext.User.Identity.Name;
            var questionExist = _unitOfWork.Questions.Exists(c => c.Id == questionId);
            var userIdClaim = _httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
            var user = _unitOfWork.Users.Get(userIdClaim);

            if (!questionExist)
            {
                response.Message = "Question does not exist!";
                return response;
            }

            var question = _unitOfWork.Questions.Get(questionId);

            if (question.UserId != user.Id)
            {
                response.Message = "You cannot update this question";
                return response;
            }

            question.QuestionText = request.QuestionText;
            question.ModifiedBy = modifiedBy;

            try
            {
                _unitOfWork.Questions.Update(question);
                _unitOfWork.SaveChanges();
                response.Message = "Question updated successfully!";
                response.Status = true;
                return response;
            }
            catch (Exception ex)
            {
                response.Message = $"Could not update the Question: {ex.Message}";
                return response;
            }
        }

        public BaseResponseModel Delete(string questionId)
        {
            var response = new BaseResponseModel();

            Expression<Func<Question, bool>> expression = (q => (q.Id == questionId)
                                        && (q.Id == questionId
                                        && q.IsDeleted == false));

            var questionExist = _unitOfWork.Questions.Exists(expression);
           

            if (!questionExist)
            {
                response.Message = "Question does not exist!";
                return response;
            }

           

            var question = _unitOfWork.Questions.Get(questionId);
            question.IsDeleted = true;

            try
            {
                _unitOfWork.Questions.Update(question);
                _unitOfWork.SaveChanges();
                response.Message = "Question deleted successfully!";
                response.Status = true;

                return response;
            }
            catch (Exception ex)
            {
                response.Message = $"Question delete failed: {ex.Message}";
                return response;
            }
        }

        public QuestionsResponseModel GetAllQuestion()
        {
            var response = new QuestionsResponseModel();

            try
            {
                var IsInRole = _httpContextAccessor.HttpContext.User.IsInRole("Admin");
                var userIdClaim = _httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
                Expression<Func<Question, bool>> expression = q => q.UserId == userIdClaim;

                var questions = IsInRole ? _unitOfWork.Questions.GetQuestions() : _unitOfWork.Questions.GetQuestions(expression);

                if (questions.Count == 0)
                {
                    response.Message = "No question found!";
                    return response;
                }

                response.Data = questions
                    .Where(q => q.IsDeleted == false)
                    .Select(question => new QuestionViewModel
                    {
                        Id = question.Id,
                        QuestionText = question.QuestionText,
                        UserName = question.User.UserName,                        
                    }).ToList();

                response.Status = true;
                response.Message = "Success";
            }
            catch (Exception ex)
            {
                response.Message = $"An error occured: {ex.StackTrace}";
                return response;
            }

            return response;
        }

        public QuestionResponseModel GetQuestion(string id)
        {
            var response = new QuestionResponseModel();
            var questionExist = _unitOfWork.Questions.Exists(q => q.Id == id && q.IsDeleted == false);
            var IsInRole = _httpContextAccessor.HttpContext.User.IsInRole("Admin");
            var userIdClaim = _httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
            var question = new Question();

            if (!questionExist)
            {
                response.Message = $"Question with id {id} does not exist!";
                return response;
            }

            question = IsInRole ? _unitOfWork.Questions.GetQuestion(q => q.Id == id && !q.IsDeleted) : _unitOfWork.Questions.GetQuestion(q => q.Id == id
                                                && q.UserId == userIdClaim
                                                && !q.IsDeleted);

            if (question is null)
            {
                response.Message = "Question not found!";
                return response;
            }

            response.Message = "Success";
            response.Status = true;
            response.Data = new QuestionViewModel
            {
                Id = question.Id,
                QuestionText = question.QuestionText,
                UserId = question.UserId,
                UserName = question.User.UserName,
                
            };

            return response;
        }

        public QuestionsResponseModel GetQuestionsBySubjectId(string subjectId)
        {
            var response = new QuestionsResponseModel();

            try
            {
                var questions = _unitOfWork.Questions.GetQuestionBySubjectId(subjectId);

                if (questions.Count == 0)
                {
                    response.Message = "No question found!";
                    return response;
                }

                response.Data = questions
                                    .Select(question => new QuestionViewModel
                                    {
                                        Id = question.Id,
                                        QuestionText = question.QuestionText,
                                        UserName = question.User.UserName,
                                        OptionA = question.OptionA,
                                        OptionB = question.OptionB,
                                        OptionC = question.OptionC,
                                        OptionD = question.OptionD,
                                    }).ToList();

                response.Status = true;
                response.Message = "Success";
            }
            catch (Exception ex)
            {
                response.Message = $"An error occured: {ex.StackTrace}";
                return response;
            }

            return response;
        }

        public QuestionsResponseModel DisplayQuestion()
        {
            var response = new QuestionsResponseModel();

            try
            {
                var questions = _unitOfWork.Questions.GetQuestions();

                if (questions.Count == 0)
                {
                    response.Message = "No question found!";
                    return response;
                }

                response.Data = questions
                    .Where(q => !q.IsDeleted)
                    .Select(question => new QuestionViewModel
                    {
                        Id = question.Id,
                        UserId = question.UserId,
                        QuestionText = question.QuestionText,
                        UserName = question.User.UserName,
                        OptionA = question.OptionA,
                        OptionB = question.OptionB,
                        OptionC = question.OptionC,
                        OptionD = question.OptionD,
                    }).ToList();

                response.Status = true;
                response.Message = "Success";
            }
            catch (Exception ex)
            {
                response.Message = $"An error occured: {ex.Message}";
                return response;
            }

            return response;
        }
    }
}
