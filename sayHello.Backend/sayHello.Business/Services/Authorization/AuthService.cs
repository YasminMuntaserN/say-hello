using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using sayHello.api;
using sayHello.api.Authorization;
using sayHello.DataAccess;
using sayHello.DTOs.Authentication;
using sayHello.DTOs.User;
using sayHello.Entities;

namespace sayHello.Business.Services;
public class AuthService
{
  
    private readonly JwtOptions _jwtOptions;
    private readonly AppDbContext _dbContext;
    private readonly IMapper _mapper;

    public AuthService(JwtOptions jwtOptions, AppDbContext dbContext , IMapper mapper)
    {
        _jwtOptions = jwtOptions;
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public async Task<(string AccessToken, string RefreshToken , UserDetailsDto user)> AuthenticateAsync(AuthRequestDto request)
    {
        var user = await _dbContext.Users
            .Include(u => u.AuthUser)
            .FirstOrDefaultAsync(x => x.Email == request.email && x.Password == request.password);
        
        if (user == null)
            return (null, null ,null);

        var accessToken = GenerateJwtToken(user);
        var refreshToken = GenerateRefreshToken();
       
        if (user.AuthUser == null)
        {
            var authUser = new AuthUser 
            { 
                UserId = user.UserId,
                RefreshToken = refreshToken,
                RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(_jwtOptions.RefreshTokenLifetimeDays)
            };
            
            _dbContext.Set<AuthUser>().Add(authUser);
            user.AuthUser = authUser;
        }
        else
        {
            user.AuthUser.RefreshToken = refreshToken;
            user.AuthUser.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(_jwtOptions.RefreshTokenLifetimeDays);
            _dbContext.AuthUsers.Update(user.AuthUser);
        }

        await _dbContext.SaveChangesAsync();
        
        return (accessToken, refreshToken ,_mapper.Map<UserDetailsDto>(user));
    }


    public async Task<(string AccessToken, string RefreshToken)> RefreshTokenAsync(string refreshToken)
    {
        var authUser = await _dbContext.AuthUsers
            .Include(a => a.User)
            .FirstOrDefaultAsync(u => u.RefreshToken == refreshToken);

       
        if (authUser == null || authUser.RefreshTokenExpiryTime <= DateTime.UtcNow)
            return (null, null);

        var newAccessToken = GenerateJwtToken(authUser.User);
        var newRefreshToken = GenerateRefreshToken();

        authUser.RefreshToken = newRefreshToken;
        authUser.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(_jwtOptions.RefreshTokenLifetimeDays);
        await _dbContext.SaveChangesAsync();

        return (newAccessToken, newRefreshToken);
    }
    
    private string GenerateJwtToken(User user)
    {
        var permissions = GetPermissionsForRole(user.Role);
        
        var claims = new List<Claim>
        {
            new(ClaimTypes.NameIdentifier, user.UserId.ToString()),
            new(ClaimTypes.Email, user.Email),
            new(ClaimTypes.Role, user.Role ?? "User"),
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
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddMinutes(_jwtOptions.AccessTokenLifetimeMinutes)
        };

        var securityToken = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(securityToken);
    }

    private string GenerateRefreshToken()
    {
        var randomNumber = new byte[64];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(randomNumber);
        return Convert.ToBase64String(randomNumber);
    }
    
    
    public async Task RevokeRefreshTokenAsync(string email)
    {
        var user = await _dbContext.Users
            .Include(u => u.AuthUser)
            .FirstOrDefaultAsync(u => u.Email == email);

        if (user?.AuthUser == null) return;

        user.AuthUser.RefreshToken = null;
        user.AuthUser.RefreshTokenExpiryTime = null;
        await _dbContext.SaveChangesAsync();
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
                  (int)Permissions.RemoveGroupMember,
        
        "GroupMember" => 
                      (int)Permissions.AuthenticateUsers | 
                      (int)Permissions.SendMessages | 
                      (int)Permissions.BlockUsers | 
                      (int)Permissions.ArchiveChats|
                      (int)Permissions.AddGroupMember| 
                      (int)Permissions.ViewChats|
                      (int)Permissions.RemoveGroupMember,
        
        "User" => (int)Permissions.SendMessages |
                  (int)Permissions.AuthenticateUsers | 
                  (int)Permissions.BlockUsers | 
                  (int)Permissions.ViewChats|
                  (int)Permissions.ArchiveChats,
        _ => 0
    };
}
