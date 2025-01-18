using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using sayHello.Business.Services;
using sayHello.DataAccess;
using sayHello.DTOs.Authentication;

namespace sayHello.api.Controllers;

[ApiController]
[Route("[controller]")] 
public class AuthController :ControllerBase
{

    private readonly AuthService _authService;

    public AuthController(AuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("login")]
    public async Task<ActionResult<TokenResponseDto>> Login([FromBody] AuthRequestDto request)
    {
        var (accessToken, refreshToken ,user) = await _authService.AuthenticateAsync(request);
        
        if (accessToken == null)
            return Unauthorized();

        return Ok(new TokenResponseDto
        {
            AccessToken = accessToken,
            RefreshToken = refreshToken,
            user = user
        });
    }

    [HttpPost("refresh")]
    [AllowAnonymous]
    public async Task<ActionResult<TokenResponseDto>> RefreshToken([FromBody] RefreshTokenRequestDto request)
    {
        try
        {
            if (string.IsNullOrEmpty(request.RefreshToken))
                return BadRequest("Refresh token is required");

            var (accessToken, refreshToken) = await _authService.RefreshTokenAsync(request.RefreshToken);

            if (accessToken == null || refreshToken == null)
                return Unauthorized(new { message = "Invalid or expired refresh token" });

            return Ok(new TokenResponseDto
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken
            });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "An error occurred while refreshing the token" });
        }
    }

    [Authorize]
    [HttpPost("revoke")]
    public async Task<IActionResult> RevokeToken()
    {
        var email = User.FindFirst(ClaimTypes.Email)?.Value;
        if (string.IsNullOrEmpty(email))
            return Unauthorized();

        await _authService.RevokeRefreshTokenAsync(email);
        return Ok();
    }
}