namespace ONLINEEXAMINATION.API.Repositorys.Interface
{
    public interface IUserOptionRepository
    {
        int UserQuestionOption(int id, int questionId, int optionId, int quizId);
    }
}
