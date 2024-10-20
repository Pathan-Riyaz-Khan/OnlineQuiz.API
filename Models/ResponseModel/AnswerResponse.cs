namespace ONLINEEXAMINATION.API.Models.ResponseModel
{
    public class AnswerResponse
    {
        public string AnswerText { get; set; }
        public bool IsCorrect { get; set; }
        public int QuestionId { get; set; }
    }
}
