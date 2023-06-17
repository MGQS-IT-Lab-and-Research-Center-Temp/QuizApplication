using Microsoft.AspNetCore.Mvc.Rendering;
using QuizApplication.Entities;
using QuizApplication.Models;
using QuizApplication.Models.Subject;
using QuizApplication.Repositories.Interface;
using QuizApplication.Service.Interface;
using System.Linq.Expressions;

namespace QuizApplication.Service.Implementation
{
    public class SubjectService : ISubjectService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public SubjectService(
            IUnitOfWork unitOfWork,
            IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
            _unitOfWork = unitOfWork;
        }

        public BaseResponseModel CreateSubject(CreateSubjectViewModel request)
        {
            var response = new BaseResponseModel();
            var createdBy = _httpContextAccessor.HttpContext.User.Identity.Name;

            var isSubjectExist = _unitOfWork.Subjects.Exists(c => c.Name == request.Name);

            if (isSubjectExist)
            {
                response.Message = "Subject already exist!";
                return response;
            }

            if (string.IsNullOrWhiteSpace(request.Name))
            {
                response.Message = "Subject name is required!";
                return response;
            }

            var subject = new Subject
            {
                Name = request.Name,
                Description = request.Description,
                CreatedBy = createdBy
            };

            try
            {
                _unitOfWork.Subjects.Create(subject);
                _unitOfWork.SaveChanges();
                response.Status = true;
                response.Message = "Subject created successfully.";

                return response;
            }
            catch (Exception ex)
            {
                response.Message = $"Failed to create subject at this time: {ex.Message}";
                return response;
            }
        }

        public BaseResponseModel DeleteSubject(string subjectId)
        {
            var response = new BaseResponseModel();
            var isSubjectExist = _unitOfWork.Subjects.Exists(c => c.Id == subjectId && !c.IsDeleted);

            if (!isSubjectExist)
            {
                response.Message = "Subject does not exist.";
                return response;
            }

            var subject = _unitOfWork.Subjects.Get(subjectId);
            subject.IsDeleted = true;

            try
            {
                _unitOfWork.Subjects.Update(subject);
                _unitOfWork.SaveChanges();
                response.Status = true;
                response.Message = "Subject successfully deleted.";

                return response;
            }
            catch (Exception ex)
            {
                response.Message = $"Can not delete this subject: {ex.Message}";
                return response;
            }
        }

        public SubjectResponseModel GetSubject(string subjectId)
        {
            var response = new SubjectResponseModel();

            Expression<Func<Subject, bool>> expression = c =>
                                                (c.Id == subjectId)
                                                && (c.Id == subjectId
                                                && c.IsDeleted == false);

            var subjectExist = _unitOfWork.Subjects.Exists(expression);

            if (!subjectExist)
            {
                response.Message = $"Subject with id {subjectId} does not exist.";
                return response;
            }

            var subject = _unitOfWork.Subjects.Get(subjectId);

            response.Message = "Success";
            response.Status = true;
            response.Data = new SubjectViewModel
            {
                Id = subject.Id,
                Name = subject.Name,
                Description = subject.Description
            };

            return response;
        }

        public BaseResponseModel UpdateSubject(string subjectId, UpdateSubjectViewModel request)
        {
            var response = new BaseResponseModel();
            string modifiedBy = _httpContextAccessor.HttpContext.User.Identity.Name;
            var subjectExist = _unitOfWork.Subjects.Exists(c => c.Id == subjectId);

            if (!subjectExist)
            {
                response.Message = "Subject does not exist.";
                return response;
            }

            var category = _unitOfWork.Subjects.Get(subjectId);
            category.Description = request.Description;
            category.ModifiedBy = modifiedBy;

            try
            {
                _unitOfWork.Subjects.Update(category);
                _unitOfWork.SaveChanges();
                response.Message = "Subject updated successfully.";

                return response;
            }
            catch (Exception ex)
            {
                response.Message = $"Could not update the Subject: {ex.Message}";
                return response;
            }
        }

        public IEnumerable<SelectListItem> SelectSubject()
        {
            return _unitOfWork.Subjects.SelectAll().Select(cat => new SelectListItem()
            {
                Text = cat.Name,
                Value = cat.Id
            });
        }
    }
}
