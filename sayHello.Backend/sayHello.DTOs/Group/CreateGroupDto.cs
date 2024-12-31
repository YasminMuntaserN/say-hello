namespace sayHello.DTOs.Group;

public class CreateGroupDto
{
    public string Name{ get; set; }
    public string? ImageUrl{ get; set; }
    public DateTime? CreatedAt { get; set; } = default;
}

