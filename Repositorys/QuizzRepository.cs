using Dapper;
using Microsoft.Extensions.Options;
using ONLINEEXAMINATION.API.Models.DBModel;
using ONLINEEXAMINATION.API.Repositorys.Interface;

namespace ONLINEEXAMINATION.API.Repositorys
{
    public class QuizzRepository : BaseRepository<Quizz>, IQuizzRepository
    {
        public QuizzRepository(IOptions<ConnectionString> connectionString)
            :base(connectionString.Value.OEDB){ }
        public int Create(Quizz quizz)
        {
            string query = "INSERT INTO Foundation.Quizzs (Title, Description, AdminId, StartTime, EndTime, Password, CreatedAt, UpdatedAt) VALUES (@Title, @Description, @AdminId, @StartTime, @EndTime, @Password, @CreatedAt, @UpdatedAt)";
            return Create(query, quizz);
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

        public int Update(int adminId, int id, Quizz quizz)
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
