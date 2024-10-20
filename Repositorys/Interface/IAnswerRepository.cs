using ONLINEEXAMINATION.API.Models.RequestModel;
using ONLINEEXAMINATION.API.Models.ResponseModel;

namespace ONLINEEXAMINATION.API.Repositorys.Interface
{
    public interface IAnswerRepository
    {
        IList<AnswerResponse> Get(int QuestionId);
        AnswerResponse GetById(int Id);
        int Create(int QuestionId, AnswerRequest request);
        int Update(int QuestionId , int id, AnswerRequest request);
        int Delete(int Id);
        IList<AnswerResponse> GetAnswerByUser(int userId);
    }
}
