using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using NtierEF.DAL.Interfaces.Repositories;

namespace NtierEF.DAL.Data.Repositories
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        protected readonly ShopDbContext _context;
        protected readonly DbSet<TEntity> table;

        public GenericRepository(ShopDbContext context)
        {
            _context = context;
            table = _context.Set<TEntity>();
        }

        public async Task<TEntity?> GetByIdAsync(int id) 
        {
            return await table.FindAsync(id);
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await table.ToListAsync();
        }

        public async Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await table.Where(predicate).ToListAsync();
        }

        public async Task AddAsync(TEntity entity)
        {
            await table.AddAsync(entity);
        }

        public void Update(TEntity entity)
        {
            table.Update(entity);
        }

        public void Delete(TEntity entity)
        {
            table.Remove(entity);
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
