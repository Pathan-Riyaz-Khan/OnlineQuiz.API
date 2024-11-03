using Dapper;
using Microsoft.Extensions.Options;
using ONLINEEXAMINATION.API.Models.DBModel;
using ONLINEEXAMINATION.API.Models.RequestModel;
using ONLINEEXAMINATION.API.Models.ResponseModel;
using ONLINEEXAMINATION.API.Repositorys.Interface;

namespace ONLINEEXAMINATION.API.Repositorys
{
    public class QuestionRepository : BaseRepository<Question>, IQuestionRepository
    {
        public QuestionRepository(IOptions<ConnectionString> connectionString)
            : base(connectionString.Value.OEDB) { }
        public int Create(Question question)
        {
            string query = "INSERT INTO Foundation.Questions (Text, QuizId, CreatedAt, UpdatedAt) VALUES (@Text, @QuizId, @CreatedAt, @UpdatedAt)";
            return Create(query, question);
        }

        public int Delete(int Id)
        {
            //string query = "DELETE FROM Foundation.Questions WHERE Id = @Id";
            //return Delete(query,new { Id = Id });
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("QuestionId", Id);
            return StoredProcedure("spDeleteQuestion", parameters);
        }

        public IList<Question> Get(int QuizId)
        {
            string query = "SELECT * FROM Foundation.Questions WHERE QuizId = " + QuizId;
            return Get(query);
        }

        public Question GetById(int Id)
        {
            string query = "SELECT * FROM Foundation.Questions WHERE Id = @Id";
            return GetById(query, new { Id = Id });
        }

        //public IList<Question> GetQuestionsByUser(int userId)
        //{
        //    string query = @"SELECT Id, QuestionText, QuizId, QuestionType
        //     FROM Foundation.Questions Q
        //     INNER JOIN Foundation.UserQuizs Uq on Q.Id = Uq.QuestionId where Uq.UserId = @UserId";
        //    return GetMultiple(query, new { UserId = userId });
        //}

        public int Update(Question question)
        {
            string query = "UPDATE FROM Foundation.Questions SET Text = @Text, QuizId = @QuizId, CreatedAt = @CreatedAt, UpdatedAt = @UpdatedAt";
            return Update(query,question);
        }
    }
}
