using System.ComponentModel.DataAnnotations;

namespace QuizApplication.Models.Question
{
    public class CreateQuestionViewModel
    {
        [Required(ErrorMessage = "One or more Subject need to be selected")]
        public List<string> SubjectIds { get; set; }

        public string QuestionAsked { get; set; }

        public string ImageUrl { get; set; }
    }
}
