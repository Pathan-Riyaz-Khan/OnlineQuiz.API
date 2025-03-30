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
        int UserQuestionOption(int id, int questionId, int optionId, int quizId);
        void Update(int Id, UserRequest request);
        void Delete(int Id);
        void CheckUser(LoginDTO userLogin);
        int GetQuizForUser(int Id, string Password);
        IList<UserQuestionResponse> GetUserQuestionOption(int id, int quizId);
        //int CheckUser(LoginDTO user);
    }
}
