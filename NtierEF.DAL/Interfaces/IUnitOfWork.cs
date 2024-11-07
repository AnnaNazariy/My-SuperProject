using System;
using System.Threading.Tasks;

namespace NtierEF.DAL.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        ICategoryRepository Categories { get; }
        IProductRepository Products { get; }
        Task<int> SaveChangesAsync();
    }
}
