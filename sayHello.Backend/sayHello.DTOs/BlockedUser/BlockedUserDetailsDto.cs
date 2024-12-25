namespace sayHello.DTOs.BlockedUser;

public record BlockedUserDetailsDto(
    int Id,
    int UserId,
    int BlockedByUserId,
    DateTime DateBlocked,
    string? Reason
);