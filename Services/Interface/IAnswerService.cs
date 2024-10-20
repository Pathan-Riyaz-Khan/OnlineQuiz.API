using ONLINEEXAMINATION.API.Models.RequestModel;
using ONLINEEXAMINATION.API.Models.ResponseModel;

namespace ONLINEEXAMINATION.API.Services.Interface
{
    public interface IAnswerService
    {
        IList<AnswerResponse> Get(int QuestionId);
        AnswerResponse GetById(int Id);
        int Create(int QuestionId, AnswerRequest request);
        void Update(int QuestionId, int id, AnswerRequest request);
        void Delete(int Id);
    }
}
