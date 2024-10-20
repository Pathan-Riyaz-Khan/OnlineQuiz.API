using Dapper;
using Microsoft.Extensions.Options;
using ONLINEEXAMINATION.API.Models.RequestModel;
using ONLINEEXAMINATION.API.Models.ResponseModel;
using ONLINEEXAMINATION.API.Repositorys.Interface;

namespace ONLINEEXAMINATION.API.Repositorys
{
    public class AnswerRepository : BaseRepository<AnswerResponse>, IAnswerRepository
    {
        public AnswerRepository(IOptions<ConnectionString> connectionString)
            :base(connectionString.Value.OEDB){ }
        public int Create(int QuestionId, AnswerRequest request)
        {
            string query = "INSERT INTO Foundation.Answers (AnswerText, IsCorrect, QuestionId) VALUES (@AnswerText, @IsCorrect, @QuestionId)";
            return Create(query, new {QuestionId = QuestionId, AnswerText = request.AnswerText, IsCorrect = request.IsCorrect});
        }

        public int Delete(int Id)
        {
            //string query = "DELETE FROM Foundation.Answers WHERE Id = @Id";
            //return Delete(query, new {Id = Id});
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("AnswerId", Id);
            return StoredProcedure("spDeleteAnswer", parameters);
        }

        public IList<AnswerResponse> Get(int QuestionId)
        {
            string query = "SELECT * FROM Foundation.Answers WHERE QuestionId = " + QuestionId;
            return Get(query);
        }

        public IList<AnswerResponse> GetAnswerByUser(int userId)
        {
            string query = @"SELECT Id, Title, AnswerText, IsCorrect
             FROM Foundation.Answers A
             INNER JOIN Foundation.UserQuizs Uq on A.Id = Uq.AnswerId where Uq.UserId = @UserId";
            return GetMultiple(query, new { UserId = userId });
        }

        public AnswerResponse GetById(int Id)
        {
            string query = "SELECT FROM Foundation.Answers WHERE Id = @Id";
            return GetById(query, new {Id = Id});
        }

        public int Update(int QuestionId, int id, AnswerRequest request)
        {
            string query = "UPDATE Foundation.Answers SET Title = @Title, AnswerText = @AnswerText, IsCorrect = @IsCorrect";
            return Update(query, new { QuestionId = QuestionId, AnswerText = request.AnswerText, IsCorrect = request.IsCorrect });
        }
    }
}
