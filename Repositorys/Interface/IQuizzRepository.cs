using ONLINEEXAMINATION.API.Models.DBModel;

namespace ONLINEEXAMINATION.API.Repositorys.Interface
{
    public interface IQuizzRepository
    {
        IList<Quizz> Get();
        Quizz GetById(int id);
        IList<Quizz> GetByAdminId(int adminId);
        int Create(Quizz quizz);
        int Update(int adminId, int id, Quizz quizz);
        int Delete(int id);
        //IList<QuizzResponse> GetQuizzsByUser(int userId);
    }
}
