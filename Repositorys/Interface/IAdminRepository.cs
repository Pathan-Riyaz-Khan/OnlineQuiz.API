using ONLINEEXAMINATION.API.Models.RequestModel;
using ONLINEEXAMINATION.API.Models.DBModel;

namespace ONLINEEXAMINATION.API.Repositorys.Interface
{
    public interface IAdminRepository
    {
        IList<Admin> Get();
        Admin GetById(int id);
        int Create(Admin adminRequest);
        int Update(int id, Admin adminRequest);
        int Delete(int id);
    }
}
