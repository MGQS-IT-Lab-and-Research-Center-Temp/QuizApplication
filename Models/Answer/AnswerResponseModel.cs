namespace QuizApplication.Models.Answer
{
    public class AnswerResponseModel : BaseResponseModel
    {
        public AnswerViewModel Data { get; set; }
    }

    public class AnswersResponseModel : BaseResponseModel
    {
        public List<AnswerViewModel> Data { get; set; }
    }
}   