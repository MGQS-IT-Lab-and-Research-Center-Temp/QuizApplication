using Microsoft.AspNetCore.Mvc.Rendering;
using QuizApplication.Models;
using QuizApplication.Models.Subject;

namespace QuizApplication.Service.Interface
{
    public interface ISubjectService
    {
        BaseResponseModel CreateSubject(CreateSubjectViewModel createSubjectDto);
        BaseResponseModel DeleteSubject(string subjectId);
        BaseResponseModel UpdateSubject(string subjectId, UpdateSubjectViewModel updateSubjectDto);
        SubjectResponseModel GetSubject(string subjectId);
		SubjectsResponseModel GetAllSubject();
		IEnumerable<SelectListItem> SelectSubject();
    }
}
