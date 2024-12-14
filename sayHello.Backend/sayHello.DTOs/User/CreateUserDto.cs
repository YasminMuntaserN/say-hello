namespace sayHello.DTOs.User;

public class CreateUserDto
{
    public string Username{ get; set; }
    public string Email{ get; set; }
    public string Password{ get; set; }
    public string? ProfilePictureUrl { get; set; }
    public string? Bio{ get; set; }
    public string Status{ get; set; }
    public DateTime DateJoined{ get; set; } = default;
};
