namespace sayHello.DTOs.BlockedUser;

public record CreateBlockedUserDto(
    int UserId,
    int BlockedByUserId,
    string? Reason
);