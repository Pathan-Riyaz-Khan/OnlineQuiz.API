using ONLINEEXAMINATION.API.Models.RequestModel;
using ONLINEEXAMINATION.API.Models.ResponseModel;

namespace ONLINEEXAMINATION.API.Services.Interface
{
    public interface IQuizzService
    {
        IList<QuizzResponse> Get();
        QuizzResponse GetById(int Id);
        IList<QuizzResponse> GetByAdminId(int AdminId);
        int Create(QuizzRequest quizzRquest);
        void Update(int AdminId, int id, QuizzRequest quizzRquest);
        void Delete(int id);
    }
}
