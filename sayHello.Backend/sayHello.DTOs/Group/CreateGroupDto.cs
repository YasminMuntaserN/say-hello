namespace sayHello.DTOs.Group;

    public record CreateGroupDto(
    string Name,
    string ImageUrl,
    DateTime? CreatedAt = default
);

