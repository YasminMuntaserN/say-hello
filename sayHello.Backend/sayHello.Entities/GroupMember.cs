namespace sayHello.Entities;

public class GroupMember
{
    public int Id;
    public int GroupId { get; set; }
    public Group Group { get; set; }

    public int UserId { get; set; }
    
    public User User { get; set; }
}