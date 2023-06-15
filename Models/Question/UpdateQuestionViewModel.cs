using System.ComponentModel.DataAnnotations;

namespace QuizApplication.Models.Question
{
    public class UpdateQuestionViewModel
    {
        public int Id { get; set; }

        public string QuestionAsked { get; set; }

        public string ImageUrl { get; set; }
    }
}
