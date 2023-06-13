namespace QuizApplication.Entities
{
    public class SubjectQuestion : BaseEntity
    {
        public string SubjectId { get; set; }
        public Subject Subject { get; set; }
        public string QuestionId { get; set; }
        public Question Question { get; set; }

    }
}
