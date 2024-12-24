namespace sayHello.DTOs.User;

public class UpdateUserDto
{
    public int UserId { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
    public string? ProfilePictureUrl { get; set; }
    public string? Bio { get; set; }
    public string Status { get; set; }
}
