namespace sayHello.Entities;

public class Group
{
    public int GroupId { get; set; }
    public string Name { get; set; }
    public string ImageUrl { get; set; }
    
    public DateTime CreatedAt { get; set; }

    public ICollection<GroupMember> Members { get; set; } = new List<GroupMember>();
    
    public ICollection<Message> Messages { get; set; } = new List<Message>();
}

