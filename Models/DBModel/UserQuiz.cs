namespace ONLINEEXAMINATION.API.Models.DBModel
{
    public class UserQuiz
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int QuizId { get; set; }
        public int Score { get; set; }
    }
}
