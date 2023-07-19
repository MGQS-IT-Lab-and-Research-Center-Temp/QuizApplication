namespace QuizApplication.Entities
{
    public class Answer : BaseEntity
    {
        public string UserId { get; set; }
        public User User { get; set; }
        public string QuestionId { get; set; }
        public Question Question { get; set; }
        public string AnswerText { get; set; }
        public string AnswerText1 { get; set; }
        public string AnswerText2 { get; set; }
        public string AnswerText3 { get; set; }



    }
}
