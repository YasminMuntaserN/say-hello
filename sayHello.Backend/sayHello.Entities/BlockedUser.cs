namespace sayHello.Entities;

public class BlockedUser
{
    public int Id { get; set; }
   
    public int UserId { get; set; }
    public User User { get; set; }
    
    public int BlockedUserId { get; set; }
    public User Blocked_User { get; set; }
}