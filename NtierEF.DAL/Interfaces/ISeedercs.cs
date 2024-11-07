using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace NtierEF.DAL.Interfaces
{
    public interface ISeeder<T> where T : class
    {
        void Seed(EntityTypeBuilder<T> builder);
    }
}
