namespace sayHello.DTOs.BlockedUser;

public record BlockedUserDetailsDto(
    int BlockedUserId,
    int UserId,
    DateTime DateBlocked,
    string? Reason
);