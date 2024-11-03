namespace ONLINEEXAMINATION.API.Models.RequestModel
{
    public class UserOptionRequest
    {
        public int QuestionId { get; set; }
        public int OptionId { get; set; }
        public int QuizId { get; set; }
    }
}
