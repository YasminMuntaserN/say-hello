namespace sayHello.Entities;

public class BlockedUser
{
    public int Id { get; set; }  

    public int UserId { get; set; }  
    public User User { get; set; } 

    public int BlockedByUserId { get; set; }
    public User BlockedByUser { get; set; } 

    public DateTime DateBlocked { get; set; }
    public string? Reason { get; set; }
    
    public bool IsBlocked { get; set; } = false;
}