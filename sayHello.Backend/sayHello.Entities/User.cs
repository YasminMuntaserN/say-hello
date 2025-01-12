namespace sayHello.Entities;

public class User
{
    public int UserId { get; set; }
    public string Username { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string ProfilePictureUrl { get; set; }
    public string? Bio { get; set; }
    public string Status { get; set; } //Online, Offline, Last Seen
    public DateTime DateJoined { get; set; }
    public bool IsDeleted { get; set; }
    public DateTime? LastLogin { get; set; }
    public string? Role { get; set; }
    public ICollection<Message> SentMessages { get; set; } = new List<Message>();
    public ICollection<Message> ReceivedMessages { get; set; } = new List<Message>();

 
    public ICollection<BlockedUser> BlockedUsers { get; set; } = new List<BlockedUser>();
    public ICollection<BlockedUser> BlockedByUsers { get; set; } = new List<BlockedUser>();
   
    public ICollection<ArchivedUser> ArchivedUsers { get; set; } = new List<ArchivedUser>();
    public ICollection<ArchivedUser> ArchivedByUsers { get; set; } = new List<ArchivedUser>();
    
}