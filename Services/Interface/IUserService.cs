using ONLINEEXAMINATION.API.Models.DTO;
using ONLINEEXAMINATION.API.Models.RequestModel;
using ONLINEEXAMINATION.API.Models.ResponseModel;

namespace ONLINEEXAMINATION.API.Services.Interface
{
    public interface IUserService
    {
        IList<UserResponse> Get();
        UserResponse GetById(int id);
        int Create(UserRequest request);
        void Update(int Id, UserRequest request);
        void Delete(int Id);
        //int CheckUser(LoginDTO user);
    }
}
