namespace QuizApplication.Entities
{
    public class Role : BaseEntity
    {
        public string ClassName { get; set; }
        public string Description { get; set; }
        public ICollection<User> Users { get; set; } = new HashSet<User>();
    }
}
