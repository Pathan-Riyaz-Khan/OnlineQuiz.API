namespace ONLINEEXAMINATION.API.Models.RequestModel
{
    public class QuizzRequest
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public int AdminId { get; set; }
        public string Password { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
    }
}
