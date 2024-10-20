using ONLINEEXAMINATION.API.Models.DBModel;
using ONLINEEXAMINATION.API.Models.RequestModel;
using ONLINEEXAMINATION.API.Models.ResponseModel;

namespace ONLINEEXAMINATION.API.Repositorys.Interface
{
    public interface IQuestionRepository
    {
        IList<Question> Get(int QuizId);
        Question GetById(int Id);
        int Create(Question question);
        int Update(Question question);
        int Delete(int Id);
        //IList<Question> GetQuestionsByUser(int UserId);
    }
}
