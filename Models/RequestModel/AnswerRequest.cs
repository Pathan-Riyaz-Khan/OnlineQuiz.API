namespace ONLINEEXAMINATION.API.Models.RequestModel
{
    public class AnswerRequest
    {
        public string AnswerText { get; set; }
        public bool IsCorrect { get; set; }
        public int QuestionId { get; set; }
    }
}
