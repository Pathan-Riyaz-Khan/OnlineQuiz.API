namespace ONLINEEXAMINATION.API.Models.RequestModel
{
    public class QuestionRequest
    {
        public string Text { get; set; }
        
        public IList<OptionRequest> Options { get; set; }
    }
}
