using ONLINEEXAMINATION.API.Models.DBModel;
using ONLINEEXAMINATION.API.Models.RequestModel;
using ONLINEEXAMINATION.API.Models.ResponseModel;

namespace ONLINEEXAMINATION.API.Services.Interface
{
    public interface IQuestionService
    {
        IList<QuestionResponse> Get(int QuizId);
        QuestionResponse GetById(int Id);
        int Create(int QuizId, QuestionRequest request);
        void Update(int QuizId, int id, QuestionRequest request);
        void Delete(int Id);
    }
}
