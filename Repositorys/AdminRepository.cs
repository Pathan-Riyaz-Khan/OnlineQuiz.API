using Dapper;
using Microsoft.Extensions.Options;
using ONLINEEXAMINATION.API.Models.DBModel;
using ONLINEEXAMINATION.API.Models.DTO;
using ONLINEEXAMINATION.API.Repositorys.Interface;

namespace ONLINEEXAMINATION.API.Repositorys
{
    public class AdminRepository : BaseRepository<Admin>, IAdminRepository
    {
        public AdminRepository(IOptions<ConnectionString> connectionString)
            : base(connectionString.Value.OEDB) { }

        public int AuthCheck(LoginDTO loginDTO)
        {
            string query = "select Id from Foundation.Admins where Email = @Email and Password = @Password";
            return Login(query, loginDTO);
        }

        public int Create(Admin admin)
        {
            string query = @"INSERT INTO Foundation.Admins (
	NAME
	,Email
	,Password
	,CreatedAt
	,UpdatedAt
	)
VALUES (
	@NAME
	,@Email
	,@Password
	,@CreatedAt
	,@UpdatedAt
	)";
            return Create(query, admin);
        }

        public int Delete(int id)
        {
            //string query = "DELETE FROM Foundation.Admins WHERE Id = @Id";
            //return Delete(query, new {Id = id});
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("AdminId", id);
            return StoredProcedure("spDeleteAdmin", parameters);
        }

        public IList<Admin> Get()
        {
            string query = "SELECT * FROM Foundation.Admins";
            return Get(query);
        }

        public Admin GetById(int id)
        {
            string query = "SELECT * FROM Foundation.Admins WHERE Id = @Id";
            return GetById(query, new { Id = id });
        }

        public int Update(int id, Admin admin)
        {
            string query = "UPDATE Foundation.Admins SET NAME = @NAME, Email = @Email, Password = @Password WHERE Id = @Id";
            return Update(query, admin);
        }
    }
}
