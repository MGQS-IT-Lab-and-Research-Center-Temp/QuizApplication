using System.ComponentModel.DataAnnotations;

namespace QuizApplication.Models.Question
{
    public class UpdateQuestionViewModel
    {
        public int Id { get; set; }

        public string QuestionText { get; set; }
    }
}
