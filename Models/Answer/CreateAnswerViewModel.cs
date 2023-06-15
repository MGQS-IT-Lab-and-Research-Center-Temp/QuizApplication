using System.ComponentModel.DataAnnotations;

namespace QuizApplication.Models.Answer
{
    public class CreateAnswerViewModel
    {
        public string UserId { get; set; }
        public string QuestionId { get; set; }
        [Required(ErrorMessage = "Answer choose cannot be empty or more than one")]
        public string AnswerChoosed { get; set; }
    }
}
