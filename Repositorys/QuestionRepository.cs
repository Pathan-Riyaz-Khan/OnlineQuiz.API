using Dapper;
using Microsoft.Extensions.Options;
using ONLINEEXAMINATION.API.Models.DBModel;
using ONLINEEXAMINATION.API.Models.RequestModel;
using ONLINEEXAMINATION.API.Models.ResponseModel;
using ONLINEEXAMINATION.API.Repositorys.Interface;
using System.Data;

namespace ONLINEEXAMINATION.API.Repositorys
{
    public class QuestionRepository : BaseRepository<Question>, IQuestionRepository
    {
        public QuestionRepository(IOptions<ConnectionString> connectionString)
            : base(connectionString.Value.OEDB) { }
        public int Create(int QuizId, QuestionRequest question)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("Text", question.Text);
            parameters.Add("QuizId", QuizId);
            parameters.Add("CreatedAt", DateTime.UtcNow);
            parameters.Add("UpdatedAt", DateTime.UtcNow);
            var optionsTable = new DataTable();
            optionsTable.Columns.Add("Text", typeof(string));
            optionsTable.Columns.Add("IsCorrect", typeof(bool));

            foreach (var option in question.Options)
            {
                optionsTable.Rows.Add(option.Text, option.IsCorrect);
            }

            // Pass as Table-Valued Parameter
            parameters.Add("Options", optionsTable.AsTableValuedParameter("OptionList"));
            parameters.Add("Id", dbType: DbType.Int32, direction: ParameterDirection.Output);
            StoredProcedure("spInsertQuestionAndOptions", parameters);
            return parameters.Get<int>("Id");
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

        public int GetCount(int QuizId)
        {
            string query = "SELECT Count(Questions.Id) FROM Foundation.Questions WHERE QuizId = " + QuizId;
            return GetScore(query, new {QuizId = QuizId});
        }

        //public IList<Question> GetQuestionsByUser(int userId)
        //{
        //    string query = @"SELECT Id, QuestionText, QuizId, QuestionType
        //     FROM Foundation.Questions Q
        //     INNER JOIN Foundation.UserQuizs Uq on Q.Id = Uq.QuestionId where Uq.UserId = @UserId";
        //    return GetMultiple(query, new { UserId = userId });
        //}

        public int Update(int QuizId, int id, QuestionRequest question)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("Text", question.Text);
            parameters.Add("QuizId", QuizId);
            parameters.Add("CreatedAt", DateTime.UtcNow);
            parameters.Add("UpdatedAt", DateTime.UtcNow);
            var optionsTable = new DataTable();
            optionsTable.Columns.Add("Text", typeof(string));
            optionsTable.Columns.Add("IsCorrect", typeof(bool));

            foreach (var option in question.Options)
            {
                optionsTable.Rows.Add(option.Text, option.IsCorrect);
            }

            // Pass as Table-Valued Parameter
            parameters.Add("Options", optionsTable.AsTableValuedParameter("OptionList"));
            parameters.Add("Id", id);
            return StoredProcedure("spUpdateQuestionAndOptions", parameters);
        }
    }
}
