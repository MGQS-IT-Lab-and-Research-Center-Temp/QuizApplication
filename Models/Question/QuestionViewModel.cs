using QuizApplication.Models.Answer;

namespace QuizApplication.Models.Question
{
    public class QuestionViewModel
    {
        public string Id { get; set; }
        public string UserId { get; set; }
        public string QuestionText { get; set; }
        public string OptionA { get; set; }
        public string OptionB { get; set; }
        public string OptionC { get; set; }
        public string OptionD { get; set; }
        public string UserName { get; set; }
        
    }
}