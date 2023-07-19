using System.ComponentModel.DataAnnotations;

namespace QuizApplication.Models.Answer
{
    public class CreateAnswerViewModel
    {
        public string UserId { get; set; }
        public string QuestionId { get; set; }
        [Required(ErrorMessage = "Answer choose cannot be empty or more than one")]
        public string AnswerChoosedA { get; set; }
        public string AnswerChoosedB { get; set; }
        public string AnswerChoosedC { get; set; }
        public string AnswerChoosedD { get; set; }


    }
}
