using Dapper;
using Microsoft.Extensions.Options;
using ONLINEEXAMINATION.API.Models.DBModel;
using ONLINEEXAMINATION.API.Repositorys.Interface;

namespace ONLINEEXAMINATION.API.Repositorys
{
    public class UserQuizRepository : BaseRepository<UserQuiz>, IUserQuizRepository
    {
        public UserQuizRepository(IOptions<ConnectionString> connectionString)
            : base(connectionString.Value.OEDB) { }

        public IList<UserQuiz> GetQuizUsers(int quizId)
        {
            string query = "SELECT UO.UserId, Count(UO.Id) as Score " +
                "From Foundation.UserOptions UO " +
                "INNER JOIN Foundation.Options O on UO.OptionId = O.Id Where O.IsCorrect = 1 and UO.QuizId = @quizId Group by UO.UserId";
            return Get(query, new {QuizId = quizId});
        }

        public int GetScore(int quizId, int userId)
        {
            string query = "SELECT Count(UO.Id) as Score " +
                "From Foundation.UserOptions " +
                "UO INNER JOIN Foundation.Options O on UO.OptionId = O.Id Where O.IsCorrect = 1 and UO.UserId = @userId and UO.QuizId = @quizId";
            return GetScore(query, new {UserId = userId, QuizId = quizId});
        }

        public int QuizsAttemptedByUser(int quizId, int userId, int score)
        {
            string query = "INSERT INTO Foundation.UserQuizs (UserId, QuizId, Score) VALUES (@userId, @QuizId, @score)";
            return Create(query, new { UserId = userId, QuizId = quizId, Score = score});
        }

        public IList<UserQuiz> GetAllUserQuizzes(int userId)
        {
            string query = "select QuizId from Foundation.UserQuizs where UserId = @userId";
            return Get(query, new { UserId = userId });
        }
    }
}
