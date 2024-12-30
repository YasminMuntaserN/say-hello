namespace sayHello.DTOs.Group;

public record GroupDetailsDto(
int groupId,
string Name,
string ImageUrl,
DateTime? CreatedAt
);