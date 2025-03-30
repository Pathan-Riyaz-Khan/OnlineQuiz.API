using Microsoft.Extensions.Options;
using ONLINEEXAMINATION.API.Models.DBModel;
using ONLINEEXAMINATION.API.Repositorys.Interface;

namespace ONLINEEXAMINATION.API.Repositorys
{
    public class UserOptionRepository : BaseRepository<UserOption>, IUserOptionRepository
    {
        public UserOptionRepository(IOptions<ConnectionString> connectionString)
            : base(connectionString.Value.OEDB) { }
        public int UserQuestionOption(int userId, int questionId, int optionId, int quizId)
        {
            string query = "INSERT INTO Foundation.UserOptions (UserId, QuestionId, OptionId, QuizId) " +
                "VALUES (@userId, @questionId, @optionId, @quizId)";
            return Create(query, new { UserId = userId, QuestionId = questionId, OptionId = optionId, QuizId = quizId});
        }

        public IList<UserOption> GetUserQuestionOptions(int userId, int quizId)
        {
            string query = "Select * from Foundation.UserOptions where UserId = @userId and QuizId = @quizId";
            return Get(query, new { UserId = userId, QuizId = quizId });
        }

    }
}
