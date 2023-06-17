using QuizApplication.Models;
using QuizApplication.Models.Answer;

namespace QuizApplication.Service.Interface
{
    public interface IAnswerService
    {
        BaseResponseModel WriteAnswer(CreateAnswerViewModel request);
       
        BaseResponseModel ChangeAnswer(string answerId, UpdateAnswerViewModel request);
        
    }
}
