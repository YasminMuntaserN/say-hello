namespace sayHello.DTOs.BlockedUser;

public record BlockedUserDetailsDto(
    int BlockedUserId,
    int UserId,
    int BlockedByUserId,
    DateTime DateBlocked,
    string? Reason
);