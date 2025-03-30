using ONLINEEXAMINATION.API.Models.DBModel;
using ONLINEEXAMINATION.API.Models.RequestModel;
using ONLINEEXAMINATION.API.Models.ResponseModel;

namespace ONLINEEXAMINATION.API.Repositorys.Interface
{
    public interface IQuestionRepository
    {
        IList<Question> Get(int QuizId);
        Question GetById(int Id);
        int Create(int QuizId, QuestionRequest question);
        int Update(int QuizId, int id, QuestionRequest question);
        int Delete(int Id);
        int GetCount(int QuizId);
        //IList<Question> GetQuestionsByUser(int UserId);
    }
}
