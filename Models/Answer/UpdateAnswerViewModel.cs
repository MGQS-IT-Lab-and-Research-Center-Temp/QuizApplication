using System.ComponentModel.DataAnnotations;

namespace QuizApplication.Models.Answer
{
    public class UpdateAnswerViewModel
    {
        public string Id { get; set; }
        [Required(ErrorMessage = "Answer Choose cannot be empty or more than one")]
        public string AnswerChoosed { get; set; }
    }
}
