namespace QuizApplication.Models.Subject
{
    public class SubjectResponseModel : BaseResponseModel
    {
        public SubjectViewModel Data { get; set; }
    }

    public class SubjectsResponseModel : BaseResponseModel
    {
        public List<SubjectViewModel> Data { get; set; }
    }
}