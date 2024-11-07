using Dapper;
using ShopDap.Entities;
using MySql.Data.MySqlClient;
using System.Data;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ShopDap.Repositories
{
    public class OrderWithUserRepository
    {
        private readonly MySqlConnection _mySqlConnection;
        private readonly IDbTransaction _dbTransaction;

        public OrderWithUserRepository(MySqlConnection mySqlConnection, IDbTransaction dbTransaction)
        {
            _mySqlConnection = mySqlConnection;
            _dbTransaction = dbTransaction;
        }

        // JOIN between Orders and Users
        public async Task<IEnumerable<OrderWithUserDetails>> GetOrdersWithUserDetailsAsync()
        {
            string sql = @"
                SELECT o.OrderID, o.UserID, o.TotalAmount, o.Status, 
                       u.UserID, u.Name, u.Email
                FROM Orders o
                INNER JOIN Users u ON o.UserID = u.UserID";

            var result = await _mySqlConnection.QueryAsync<OrderWithUserDetails>(
                sql, transaction: _dbTransaction);

            return result;
        }
    }
}
