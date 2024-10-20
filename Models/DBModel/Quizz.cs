namespace ONLINEEXAMINATION.API.Models.DBModel
{
    public class Quizz
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int AdminId { get; set; }
        public string Password { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
    }
}
