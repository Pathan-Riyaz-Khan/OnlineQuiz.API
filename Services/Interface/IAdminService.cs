using ONLINEEXAMINATION.API.Models.DTO;
using ONLINEEXAMINATION.API.Models.RequestModel;
using ONLINEEXAMINATION.API.Models.ResponseModel;

namespace ONLINEEXAMINATION.API.Services.Interface
{
    public interface IAdminService
    {
        IList<AdminResponse> Get();
        AdminResponse GetById(int id);
        void Login(LoginDTO loginDTO);
        int Create(AdminRequest request);
        void Update(int Id, AdminRequest request);
        void Delete(int Id);
    }
}
