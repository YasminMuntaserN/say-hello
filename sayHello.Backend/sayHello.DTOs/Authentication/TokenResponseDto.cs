using sayHello.DTOs.User;
using sayHello.Entities;

public class TokenResponseDto
{
    public string AccessToken { get; set; }
    public string RefreshToken { get; set; }
    public UserDetailsDto user { get; set; }
}