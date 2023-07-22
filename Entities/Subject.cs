namespace QuizApplication.Entities
{
    public class Subject : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public ICollection<Question> Questions { get; set; } = new HashSet<Question>();
    }
}
