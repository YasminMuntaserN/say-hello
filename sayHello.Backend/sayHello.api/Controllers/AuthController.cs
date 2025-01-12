using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
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
    
    [HttpPost]
    [Route("login")]
    public async Task<ActionResult<string>> Login(AuthRequestDto request)
    {
        if (request == null)
            return BadRequest();

        var token = await _authService.AuthenticateAsync(request);

        if (token == null)
            return Unauthorized();

        return token;
    }
}