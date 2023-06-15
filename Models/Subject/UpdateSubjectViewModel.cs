using System.ComponentModel.DataAnnotations;

namespace QuizApplication.Models.Subject
{
    public class UpdateSubjectViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "You must pick at least a Subject")]
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
