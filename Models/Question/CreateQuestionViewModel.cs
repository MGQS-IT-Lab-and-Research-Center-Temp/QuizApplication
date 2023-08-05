using System.ComponentModel.DataAnnotations;

namespace QuizApplication.Models.Question
{
    public class CreateQuestionViewModel
    {
        [Required(ErrorMessage = "One or more Subject need to be selected")]
        public string SubjectId { get; set; }
        public string QuestionText { get; set; }
        public string OptionA { get; set; }
        public string OptionB { get; set; }
        public string OptionC { get; set; }
        public string OptionD { get; set; }
        public int CorrectOption { get; set; }
    }
}
