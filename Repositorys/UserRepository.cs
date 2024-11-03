using Dapper;
using Microsoft.Extensions.Options;
using ONLINEEXAMINATION.API.Models.DBModel;
using ONLINEEXAMINATION.API.Models.DTO;
using ONLINEEXAMINATION.API.Models.RequestModel;
using ONLINEEXAMINATION.API.Models.ResponseModel;
using ONLINEEXAMINATION.API.Repositorys.Interface;

namespace ONLINEEXAMINATION.API.Repositorys
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(IOptions<ConnectionString> connectionString)
            :base(connectionString.Value.OEDB){ }

        //public int checkUser(LoginDTO user)
        //{
        //    string query = "SELECT Id FROM Foundation.Users WHERE Email = @Email and Password = @Password";
        //    return Login(query, user);
        //}

        public int Create(User user)
        {
            
            string query = "INSERT INTO Foundation.Users (NAME, Email, Password, CreatedAt, UpdatedAt) VALUES (@NAME, @Email, @Password, @CreatedAt, @UpdatedAt)";
                                                                                                                                                                        
            return Create(query, user);
        }

        public int Delete(int id)
        {
            //string query = "DELETE FROM Foundation.Users WHERE Id = @Id";
            //return Delete(query, new {Id = id});
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("UserId", id);
            return StoredProcedure("spDeleteUser", parameters);
        }

        public IList<User> Get()
        {
            string query = "SELECT * FROM Foundation.Users";
            return Get(query);
        }

        public User GetById(int id)
        {
            string query = "SELECT * FROM Foundation.Users WHERE Id = @Id";
            return GetById(query, new { Id = id });
        }

        //public IList<User> GetUsersByQuizId(int quizId)
        //{
        //    string query = "select * from Foundation.Users U Inner Join Foundation.UserQuizs UQ on U.Id = UQ.UserId where UQ.QuizId = @quizId";
        //    return Get(query, new {quizId = quizId});
        //}

        public int Update(int id, User user)
        {
            string query = "UPDATE Foundation.Users SET NAME = @NAME, DOB = @DOB, Email = @Email, Password = @Password WHERE Id = @Id";
            return Update(query, user);
        }
    }
}
