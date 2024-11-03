using ONLINEEXAMINATION.API.Models.DBModel;
using ONLINEEXAMINATION.API.Models.RequestModel;
using ONLINEEXAMINATION.API.Models.ResponseModel;

namespace ONLINEEXAMINATION.API.Repositorys.Interface
{
    public interface IOptionRepository
    {
        IList<Option> Get(int QuestionId);
        Option GetById(int Id);
        int Create(int QuestionId, Option request);
        int Update(int QuestionId , int id, Option request);
        int Delete(int Id);
    }
}
