namespace sayHello.Entities;

public class User
{
    public int UserId { get; set; }
    public string Username { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string ProfilePictureUrl { get; set; }
    public string Bio { get; set; }
    public string Status { get; set; } //Online, Offline, Last Seen
    public DateTime DateJoined { get; set; }
    public bool IsDeleted { get; set; }
    public DateTime? LastLogin { get; set; }
}