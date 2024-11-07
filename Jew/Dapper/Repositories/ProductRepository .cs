using Dapper;
using Dapper_Example.DAL.Entities;
using System.Collections.Generic;
using Dapper_Example.DAL.Connection;
using System.Data;
using MySql.Data.MySqlClient;
using NtierEF.DAL.Entities;
using NtierEF.DAL.Repositories.Interfaces;

namespace Dapper.Repositories
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        public ProductRepository(MySqlConnection mySqlConnection, IDbTransaction dbTransaction)
            : base(mySqlConnection, dbTransaction, "products")
        {
        }

        public async Task<IEnumerable<Product>> ProductByCategoryAsync(int categoryId)
        {
            string sql = "SELECT * FROM Products WHERE CategoryId = @CategoryId";
            var results = await _mySqlConnection.QueryAsync<Product>(sql,
                new { CategoryId = categoryId },
                transaction: _dbTransaction);
            return results;
        }
    }
}
