using Dapper;
using System.Data.SqlClient;
namespace ONLINEEXAMINATION.API.Repositorys
{
    public class BaseRepository<T>
    {
        private readonly string _connectionString;
        public BaseRepository(string connectionString)
        {
            _connectionString = connectionString;
        }
        public int Create(string query, object parameters)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                return connection.Execute(query, parameters);
            }
        }

        public IList<T> Get(string query, object? parameters = null)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                return connection.Query<T>(query, parameters).ToList();
            }
        }
        public T GetById(string query, object parameters)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                return connection.QueryFirstOrDefault<T>(query, parameters);
            }
        }
        public int Update(string query, object parameters)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                return connection.Execute(query, parameters);
            }
        }
        public int Delete(string query, object parameters)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                return connection.Execute(query, parameters);
            }
        }
        public IList<T> GetMultiple(string query, object parameters)
        {
            using var connection = new SqlConnection(_connectionString);
            return connection.Query<T>(query, parameters).ToList();
        }
        public int StoredProcedure(string query, DynamicParameters parameters)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                return connection.Execute(query, parameters, commandType: System.Data.CommandType.StoredProcedure);
            }
        }
        
        public int Login(string query, object parameters)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                return connection.QueryFirstOrDefault<int>(query, parameters);
            }
        }
    }
}

