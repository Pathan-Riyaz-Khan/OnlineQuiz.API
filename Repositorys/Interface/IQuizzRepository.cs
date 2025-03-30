using ONLINEEXAMINATION.API.Models.DBModel;

namespace ONLINEEXAMINATION.API.Repositorys.Interface
{
    public interface IQuizzRepository
    {
        IList<Quizz> Get();
        Quizz GetById(int id);
        IList<Quizz> GetByAdminId(int adminId);
        int Create(Quizz quizz);
        IList<Quizz> GetQuizzsByUserId(int userId);
        int Update(int id, Quizz quizz);

        int CheckQuizCredintals(int Id, string Password);
        int Delete(int id);
        //IList<QuizzResponse> GetQuizzsByUser(int userId);
    }
}
