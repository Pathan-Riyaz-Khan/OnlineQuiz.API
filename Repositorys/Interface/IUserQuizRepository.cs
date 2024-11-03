using ONLINEEXAMINATION.API.Models.DBModel;

namespace ONLINEEXAMINATION.API.Repositorys.Interface
{
    public interface IUserQuizRepository
    {
        int GetScore(int quizId, int userId);
        int QuizsAttemptedByUser(int id, int userId, int score);
        IList<UserQuiz> GetQuizUsers(int quizId);
    }
}
