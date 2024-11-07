using AuthMicroservice.Models;

namespace AuthMicroservice.Repository
{
    public interface IUserRepository
    {
        Task<ApplicationUser> GetUserByIdAsync(string userId);
        Task<ApplicationUser> GetUserByEmailAsync(string email);
    }

}
