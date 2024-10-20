namespace ONLINEEXAMINATION.API.Models.ResponseModel
{
    public class QuestionResponse
    {
        public string Text { get; set; }
        public int QuizId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
