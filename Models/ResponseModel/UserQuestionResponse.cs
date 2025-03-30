namespace ONLINEEXAMINATION.API.Models.ResponseModel
{
    public class UserQuestionResponse
    {
        public int Id { get; set; }
        public string text { get; set; }
        public IList<OptionResponse> Options { get; set; }
        public string selectedOption { get; set; }
    }
}
