using System.ComponentModel;
using System.Data.SqlClient;
using System.Data;
using System.Reflection;
using System.Text;
using Dapper.Repositories.Interfaces;
using Dapper;
using Dapper.Entities;

namespace Dapper.Repositories
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(MySqlConnection mySqlConnection, IDbTransaction dbTransaction)
            : base(mySqlConnection, dbTransaction, "users")
        {
        }

        public async Task<User> GetUserByEmailAsync(string email)
        {
            string sql = "SELECT * FROM Users WHERE Email = @Email";
            var user = await _mySqlConnection.QuerySingleOrDefaultAsync<User>(sql,
                new { Email = email },
                transaction: _dbTransaction);
            return user;
        }
    }
}
