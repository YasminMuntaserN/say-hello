using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using sayHello.api;
using sayHello.api.Authorization;
using sayHello.DataAccess;
using sayHello.DTOs.Authentication;
using sayHello.Entities;

namespace sayHello.Business.Services;
public class AuthService
{
    private readonly JwtOptions _jwtOptions;
    private readonly AppDbContext _dbContext;

    public AuthService(JwtOptions jwtOptions, AppDbContext dbContext)
    {
        _jwtOptions = jwtOptions;
        _dbContext = dbContext;
    }

    public async Task<string> AuthenticateAsync(AuthRequestDto request)
    {
        var user = await _dbContext.Users
            .FirstOrDefaultAsync(x => x.Username == request.username && x.Password == request.password);
        
        if (user == null)
            return null;

        return GenerateJwtToken(user);
    }

    private string GenerateJwtToken(User user)
    {
        var permissions = GetPermissionsForRole(user.Role);
        
        var claims = new List<Claim>
        {
            new(ClaimTypes.NameIdentifier, user.UserId.ToString()),
            new(ClaimTypes.Name, user.Username),
            new(ClaimTypes.Role, user.Role),
            new("Permissions", permissions.ToString()) 
        };
        
        var tokenHandler = new JwtSecurityTokenHandler();
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Issuer = _jwtOptions.Issuer,
            Audience = _jwtOptions.Audience,
            SigningCredentials = new SigningCredentials(
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.SignKey)),
                SecurityAlgorithms.HmacSha256),
            Subject = new ClaimsIdentity(claims)
        };

        var securityToken = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(securityToken);
    }
    
    private int GetPermissionsForRole(string role) => role switch
    {
        "Admin" => (int)Permissions.View | 
                  (int)Permissions.ManageUsers | 
                  (int)Permissions.SendMessages | 
                  (int)Permissions.ManageGroups | 
                  (int)Permissions.BlockUsers | 
                  (int)Permissions.ArchiveChats |
                  (int)Permissions.AddGroupMember| 
                  (int)Permissions.ViewChats|
                  (int)Permissions.RemoveGroupMember ,
        
        "GroupMember" => 
                      (int)Permissions.AuthenticateUsers | 
                      (int)Permissions.SendMessages | 
                      (int)Permissions.BlockUsers | 
                      (int)Permissions.ArchiveChats|
                      (int)Permissions.AddGroupMember| 
                      (int)Permissions.ViewChats|
                      (int)Permissions.RemoveGroupMember ,
        
        "User" => (int)Permissions.SendMessages |
                  (int)Permissions.AuthenticateUsers | 
                  (int)Permissions.BlockUsers | 
                  (int)Permissions.ViewChats|
                 (int)Permissions.ArchiveChats,
        _ => 0
    };
}
    