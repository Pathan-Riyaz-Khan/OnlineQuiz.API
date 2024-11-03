using ONLINEEXAMINATION.API.Models.RequestModel;
using ONLINEEXAMINATION.API.Models.ResponseModel;

namespace ONLINEEXAMINATION.API.Services.Interface
{
    public interface IOptionService
    {
        IList<OptionResponse> Get(int QuestionId);
        OptionResponse GetById(int Id);
        int Create(int QuestionId, OptionRequest request);
        void Update(int QuestionId, int id, OptionRequest request);
        void Delete(int Id);
    }
}
