namespace QuizApplication.Entities
{
    public class Question : BaseEntity
    {
        public string QuestionText { get; set; }
        public string OptionA { get; set; }
        public string OptionB { get; set; }
        public string OptionC { get; set; }
        public string OptionD { get; set; }
        public int CorrectOption { get; set; }
        public string UserId { get; set; }
        public User User { get; set; }
        public string SubjectId { get; set; }
        public Subject Subject { get; set; }
       
       
    }
}
