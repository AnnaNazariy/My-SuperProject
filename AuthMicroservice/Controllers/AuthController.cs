using AuthMicroservice.Services;
using AuthMicroservice.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;
using System;

namespace AuthMicroservice.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IUserServices _userService;

        public AuthController(IUserServices userService)
        {
            _userService = userService;
        }

        [HttpPost("refresh-token")]
        public async Task<IActionResult> RefreshToken()
        {
            var refreshToken = Request.Cookies["refreshToken"];
            if (string.IsNullOrEmpty(refreshToken))
            {
                return BadRequest(new { Message = "Refresh token is missing or invalid." });
            }

            var response = await _userService.RefreshTokenAsync(refreshToken);

            if (response.IsAuthenticated)
            {
                SetRefreshTokenInCookie(response.RefreshToken);
                return Ok(response);
            }
            else
            {
                return Unauthorized(new { Message = response.Message });
            }
        }

        [Authorize]
        [HttpPost("revoke-token")]
        public async Task<IActionResult> RevokeToken([FromBody] RevokeTokenRequest model)
        {
            var token = model.Token ?? Request.Cookies["refreshToken"];

            if (string.IsNullOrEmpty(token))
                return BadRequest(new { message = "Token is required" });

            var response = await _userService.RevokeToken(token);

            if (!response)
                return NotFound(new { message = "Token not found or already revoked" });

            return Ok(new { message = "Token revoked successfully" });
        }

        

        private void SetRefreshTokenInCookie(string refreshToken)
        {
            var cookieOptions = new Microsoft.AspNetCore.Http.CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = Microsoft.AspNetCore.Http.SameSiteMode.Strict,
                Expires = DateTime.UtcNow.AddDays(10)
            };

            Response.Cookies.Append("refreshToken", refreshToken, cookieOptions);
        }

        
        [Authorize]
        [HttpGet("tokens/{id}")]
        public async Task<IActionResult> GetRefreshTokens(string id)
        {
            try
            {
                var refreshTokens = await _userService.GetRefreshTokensAsync(id);
                return Ok(refreshTokens);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }
    }
}
