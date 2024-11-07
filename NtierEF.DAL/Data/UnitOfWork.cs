using System.Threading.Tasks;
using NtierEF.DAL.Interfaces;
using NtierEF.DAL.Repositories;

namespace NtierEF.DAL
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ShopDbContext _context;

        public UnitOfWork(ShopDbContext context)
        {
            _context = context;
            Categories = new CategoryRepository(context);
            Products = new ProductRepository(context);
        }

        public ICategoryRepository Categories { get; }
        public IProductRepository Products { get; }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
