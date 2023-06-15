namespace QuizApplication.Entities
{
    public class Question : BaseEntity
    {
        public string UserId { get; set; }
        public User User { get; set; }
        public string AskedQuestion { get; set; }
        public string ImageUrl { get; set; }
        public bool IsClosed { get; set; }
        public ICollection<SubjectQuestion> SubjectQuestions { get; set; } = new HashSet<SubjectQuestion>();
        public ICollection<Answer> Answer { get; set; } = new HashSet<Answer>();
       
    }
}
