using ONLINEEXAMINATION.API.Models.DBModel;

namespace ONLINEEXAMINATION.API.Repositorys.Interface
{
    public interface IUserOptionRepository
    {
        int UserQuestionOption(int id, int questionId, int optionId, int quizId);
        public IList<UserOption> GetUserQuestionOptions(int userId, int quizId);
    }
}
