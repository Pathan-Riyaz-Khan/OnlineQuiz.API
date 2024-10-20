using ONLINEEXAMINATION.API.Models.DBModel;
using ONLINEEXAMINATION.API.Models.DTO;
using ONLINEEXAMINATION.API.Models.RequestModel;
using ONLINEEXAMINATION.API.Models.ResponseModel;

namespace ONLINEEXAMINATION.API.Repositorys.Interface
{
    public interface IUserRepository
    {
        IList<User> Get();
        User GetById(int id);
        int Create(User user);
        int Update(int id, User user);
        int Delete(int id);
        //int checkUser(LoginDTO user);
    }
}
