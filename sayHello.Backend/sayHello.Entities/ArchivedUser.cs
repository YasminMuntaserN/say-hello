namespace sayHello.Entities;

public class ArchivedUser
{
    public int Id { get; set; }
    public DateTime DateArchived { get; set; }
   
    public int UserId { get; set; }
    public User User { get; set; }
   
    public int ArchivedUserId { get; set; }
    public User Archived_User { get; set; }
    
    public bool IsArchived { get; set; } = false;
}