using AuthMicroservice.Data;
using AuthMicroservice.Entities;
using AuthMicroservice.Models;
using AuthMicroservice.Profile;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using AuthMicroservice.Repository;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace AuthMicroservice.Services
{
    public class UserServices : IUserServices
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _configuration;
        private readonly IUserRepository _userRepository;  

        public UserServices(UserManager<ApplicationUser> userManager, ApplicationDbContext context, IConfiguration configuration, IUserRepository userRepository)
        {
            _userManager = userManager;
            _context = context;
            _configuration = configuration;
            _userRepository = userRepository;  
        }

        public async Task<AuthenticationModel> RefreshTokenAsync(string token)
        {
            var authenticationModel = new AuthenticationModel();

            try
            {
                var principal = GetPrincipalFromExpiredToken(token);
                if (principal == null)
                {
                    authenticationModel.IsAuthenticated = false;
                    authenticationModel.Message = "Invalid refresh token.";
                    return authenticationModel;
                }

                var username = principal.Identity.Name;
                var user = await _userManager.FindByNameAsync(username);
                if (user == null)
                {
                    authenticationModel.IsAuthenticated = false;
                    authenticationModel.Message = "User not found.";
                    return authenticationModel;
                }

                var refreshToken = user.RefreshTokens.FirstOrDefault(rt => rt.Token == token && rt.IsActive);
                if (refreshToken == null || refreshToken.Expires < DateTime.UtcNow)
                {
                    authenticationModel.IsAuthenticated = false;
                    authenticationModel.Message = "Refresh token expired or invalid.";
                    return authenticationModel;
                }

                JwtSecurityToken jwtSecurityToken = await CreateJwtToken(user);
                authenticationModel.IsAuthenticated = true;
                authenticationModel.Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
                authenticationModel.Email = user.Email;
                authenticationModel.UserName = user.UserName;

                var newRefreshToken = CreateRefreshToken();
                authenticationModel.RefreshToken = newRefreshToken.Token;
                authenticationModel.RefreshTokenExpiration = newRefreshToken.Expires;

                user.RefreshTokens.Add(newRefreshToken);
                _context.Update(user);
                await _context.SaveChangesAsync();

                return authenticationModel;
            }
            catch (Exception ex)
            {
                authenticationModel.IsAuthenticated = false;
                authenticationModel.Message = ex.Message;
                return authenticationModel;
            }
        }

        

        public async Task<RefreshToken[]> GetRefreshTokensAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                throw new Exception("User not found");
            }
            return user.RefreshTokens.ToArray();
        }

        private ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            try
            {
                var jwtToken = tokenHandler.ReadToken(token) as JwtSecurityToken;
                var key = Encoding.UTF8.GetBytes(_configuration["JWT:Key"]);
                var validationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidIssuer = _configuration["JWT:Issuer"],
                    ValidAudience = _configuration["JWT:Audience"],
                    ValidateLifetime = false
                };

                var principal = tokenHandler.ValidateToken(token, validationParameters, out var validatedToken);
                return principal;
            }
            catch
            {
                return null;
            }
        }

        private async Task<JwtSecurityToken> CreateJwtToken(ApplicationUser user)
        {
            var claims = new List<Claim>(await _userManager.GetClaimsAsync(user));
            var roles = await _userManager.GetRolesAsync(user);

            foreach (var role in roles)
            {
                claims.Add(new Claim("role", role));
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["JWT:Issuer"],
                audience: _configuration["JWT:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: creds
            );

            return token;
        }

        private RefreshToken CreateRefreshToken()
        {
            var randomNumber = new byte[32];
            using (var generator = new RNGCryptoServiceProvider())
            {
                generator.GetBytes(randomNumber);
                return new RefreshToken
                {
                    Token = Convert.ToBase64String(randomNumber),
                    Expires = DateTime.UtcNow.AddDays(10),
                    Created = DateTime.UtcNow
                };
            }
        }

        public async Task<bool> RevokeToken(string token)
        {
            var user = _context.Users.FirstOrDefault(u => u.RefreshTokens.Any(rt => rt.Token == token));
            if (user == null)
            {
                return false;
            }

            var refreshToken = user.RefreshTokens.FirstOrDefault(rt => rt.Token == token);
            if (refreshToken == null || !refreshToken.IsActive)
            {
                return false;
            }

            refreshToken.Revoked = DateTime.UtcNow;

            _context.Update(user);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<AuthenticationModel> GetTokenAsync(TokenRequestModel model)
        {
            var authenticationModel = new AuthenticationModel();
            var user = await _userManager.FindByEmailAsync(model.Email);

            if (user == null)
            {
                authenticationModel.IsAuthenticated = false;
                authenticationModel.Message = $"No Accounts Registered with {model.Email}.";
                return authenticationModel;
            }

            if (await _userManager.CheckPasswordAsync(user, model.Password))
            {
                authenticationModel.IsAuthenticated = true;

                JwtSecurityToken jwtSecurityToken = await CreateJwtToken(user);
                authenticationModel.Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
                authenticationModel.Email = user.Email;
                authenticationModel.UserName = user.UserName;

                var rolesList = await _userManager.GetRolesAsync(user).ConfigureAwait(false);
                authenticationModel.Roles = rolesList.ToList();

                if (user.RefreshTokens.Any(a => a.IsActive))
                {
                    var activeRefreshToken = user.RefreshTokens.FirstOrDefault(a => a.IsActive);
                    authenticationModel.RefreshToken = activeRefreshToken?.Token;
                }
                else
                {
                    var newRefreshToken = CreateRefreshToken();
                    user.RefreshTokens.Add(newRefreshToken);
                    _context.Update(user);
                    await _context.SaveChangesAsync();

                    authenticationModel.RefreshToken = newRefreshToken.Token;
                    authenticationModel.RefreshTokenExpiration = newRefreshToken.Expires;
                }
            }
            else
            {
                authenticationModel.IsAuthenticated = false;
                authenticationModel.Message = "Invalid Credentials.";
            }

            return authenticationModel;
        }
    }
}
