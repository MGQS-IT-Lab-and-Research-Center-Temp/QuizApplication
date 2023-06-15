using QuizApplication.Models.Question;
using QuizApplication.Models;

namespace QuizApplication.Service.Interface
{
    public interface IQuestionService
    {
        BaseResponseModel Create(CreateQuestionViewModel createQuestionDto);
        BaseResponseModel Delete(string questionId);
        BaseResponseModel Update(string questionId, UpdateQuestionViewModel updatequestionDto);
        QuestionResponseModel GetQuestion(string questionId);
        QuestionsResponseModel GetAllQuestion();
        QuestionsResponseModel GetQuestionsBySubjectId(string 
            subjectId);
        QuestionsResponseModel DisplayQuestion();
    }
}
