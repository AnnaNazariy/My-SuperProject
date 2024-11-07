using AuthMicroservice.Models;
using AuthMicroservice.Controllers;
using System;

namespace AuthMicroservice.Entities
{
    public class RefreshToken
    {
        public int Id { get; set; }  
        public string Token { get; set; }  
        public DateTime Expires { get; set; }  
        public DateTime Created { get; set; }  
        public bool IsActive => !IsRevoked && !IsExpired;  
        public bool IsRevoked { get; set; } 
        public bool IsExpired => DateTime.UtcNow >= Expires;  
        public DateTime? Revoked { get; set; }  
        public string? RevokedReason { get; set; } 
        public string ApplicationUserId { get; set; }  
        public ApplicationUser ApplicationUser { get; set; }  
        public List<RefreshToken> RefreshTokens { get; set; } = new List<RefreshToken>();

    }
}
