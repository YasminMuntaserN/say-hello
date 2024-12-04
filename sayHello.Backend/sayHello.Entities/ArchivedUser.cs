namespace sayHello.Entities;

public class ArchivedUser
{
    public int Id { get; set; }
  
    public int UserId { get; set; }
    public User User { get; set; }
   
    public int ArchivedUserId { get; set; }
    public User Archived_User { get; set; }
}