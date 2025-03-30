using Dapper;
using Microsoft.Extensions.Options;
using ONLINEEXAMINATION.API.Models.DBModel;
using ONLINEEXAMINATION.API.Repositorys.Interface;
using System.Data;

namespace ONLINEEXAMINATION.API.Repositorys
{
    public class QuizzRepository : BaseRepository<Quizz>, IQuizzRepository
    {
        public QuizzRepository(IOptions<ConnectionString> connectionString)
            :base(connectionString.Value.OEDB){ }

        public int CheckQuizCredintals(int Id, string Password)
        {
            string query = "Select Id from Foundation.Quizzs where Id = @Id and Password = @Password";
            return GetScore(query, new {Id =  Id,Password =  Password });
        }

        public int Create(Quizz quizz)
        {
            DynamicParameters dynamicParameters = new DynamicParameters();
            dynamicParameters.Add("Title",quizz.Title);
            dynamicParameters.Add("Description", quizz.Description);
            dynamicParameters.Add("CreatedAt", quizz.CreatedAt);
            dynamicParameters.Add("UpdatedAt", quizz.UpdatedAt);
            dynamicParameters.Add("StartTime", quizz.StartTime);
            dynamicParameters.Add("EndTime", quizz.EndTime);
            dynamicParameters.Add("Password", quizz.Password);
            dynamicParameters.Add("AdminId", quizz.AdminId);
            dynamicParameters.Add("Id", dbType: DbType.Int32, direction: ParameterDirection.Output);
            StoredProcedure("insertQuizz", dynamicParameters);
            return dynamicParameters.Get<int>("Id");

        }

        public int Delete(int id)
        {
            //string query = "DELETE FROM Foundation.Quizzs WHERE Id = @Id";
            //return Delete(query, new { Id = id});
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("QuizId", id);
            return StoredProcedure("spDeleteQuiz", parameters);
        }
        

        public IList<Quizz> Get()
        {
            string query = "SELECT * FROM Foundation.Quizzs";
            return Get(query);
        }

        public IList<Quizz> GetByAdminId(int adminId)
        {
            string query = "SELECT * FROM Foundation.Quizzs where AdminId = @AdminId";
            return Get(query, new {AdminId = adminId});
        }

        public Quizz GetById(int id)
        {
            string query = "SELECT * FROM Foundation.Quizzs WHERE Id = @Id";
            return GetById(query, new { Id = id });
        }

        public IList<Quizz> GetQuizzsByUserId(int userId)
        {
            string query = "select Q.* from " +
                "Foundation.Quizzs Q " +
                "Inner Join Foundation.UserQuizs UQ on Q.Id = UQ.QuizId Where UQ.UserId = @userId";
            return Get(query, new {UserId = userId});
        }

        public int Update(int id, Quizz quizz)
        {
            string query = "UPDATE Foundation.Quizzs SET Title = @Title, Descriptions = @Descriptions, AdminId = @AdminId, StartTime = @StartTime, EndTime = @EndTime";
            return Update(query, quizz);
        }
        //public IList<QuizzResponse> GetQuizzsByUser(int userId)
        //{
        //    string query = @"SELECT Id, Title, Description, AdminId, StartTime, EndTime
        //     FROM Foundation.Quizzs Q
        //     INNER JOIN Foundation.UserQuizs Uq on Q.Id = Uq.QuizId where Uq.UserId = @UserId";
        //    return GetMultiple(query, new {UserId = userId});
        //}

    }
}
