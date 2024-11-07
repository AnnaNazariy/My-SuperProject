using AuthMicroservice.Entities;
using AuthMicroservice.Models;
using System.Threading.Tasks;

namespace AuthMicroservice.Services
{
    public interface IUserServices
    {
        Task<AuthenticationModel> GetTokenAsync(TokenRequestModel model);
        Task<AuthenticationModel> RefreshTokenAsync(string token);
        Task<RefreshToken[]> GetRefreshTokensAsync(string userId);
        Task<bool> RevokeToken(string token);
    }
}
