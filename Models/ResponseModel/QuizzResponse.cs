﻿
namespace ONLINEEXAMINATION.API.Models.ResponseModel
{
    public class QuizzResponse
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Password { get; set; }
        public int QuestionCount { get; set; }
        public int Accuracy { get; set; }
        public IList<UserQuizResponse> Users { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
    }
}
