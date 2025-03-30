namespace ONLINEEXAMINATION.API.Models.ResponseModel
{
    public class QuestionResponse
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public int QuizId { get; set; }
        public IList<OptionResponse> Options { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
