namespace ONLINEEXAMINATION.API.Models.ResponseModel
{
    public class UserQuestionOptionResponse
    {
        public string QuizName { get; set; }
        public IList<UserQuestionResponse> questions { get; set; }
    }
}
