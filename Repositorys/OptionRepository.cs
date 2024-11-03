using Dapper;
using Microsoft.Extensions.Options;
using ONLINEEXAMINATION.API.Models.DBModel;
using ONLINEEXAMINATION.API.Models.RequestModel;
using ONLINEEXAMINATION.API.Models.ResponseModel;
using ONLINEEXAMINATION.API.Repositorys.Interface;

namespace ONLINEEXAMINATION.API.Repositorys
{
    public class OptionRepository : BaseRepository<Option>, IOptionRepository
    {
        public OptionRepository(IOptions<ConnectionString> connectionString)
            :base(connectionString.Value.OEDB){ }
        public int Create(int QuestionId, Option option)
        {
            string query = "INSERT INTO Foundation.Options (Text, IsCorrect, QuestionId) VALUES (@Text, @IsCorrect, @QuestionId)";
            return Create(query, option);
        }

        public int Delete(int Id)
        {
            //string query = "DELETE FROM Foundation.Answers WHERE Id = @Id";
            //return Delete(query, new {Id = Id});
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("AnswerId", Id);
            return StoredProcedure("spDeleteAnswer", parameters);
        }

        public IList<Option> Get(int QuestionId)
        {
            string query = "SELECT * FROM Foundation.Options WHERE QuestionId = " + QuestionId;
            return Get(query);
        }

        //public IList<OptionResponse> GetAnswerByUser(int userId)
        //{
        //    string query = @"SELECT Id, Title, Text, IsCorrect
        //     FROM Foundation.Options A
        //     INNER JOIN Foundation.UserQuizs Uq on A.Id = Uq.AnswerId where Uq.UserId = @UserId";
        //    return GetMultiple(query, new { UserId = userId });
        //}

        public Option GetById(int Id)
        {
            string query = "SELECT * FROM Foundation.Options WHERE Id = @Id";
            return GetById(query, new {Id = Id});
        }

        public int Update(int QuestionId, int id, Option option)
        {
            string query = "UPDATE Foundation.Options Text = @Text, IsCorrect = @IsCorrect";
            return Update(query, option);
        }
    }
}
