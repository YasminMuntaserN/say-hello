namespace sayHello.DTOs.User;

public record UserDetailsDto(
    int UserId,
    string Username,
    string Email,
    string Password,
    string ProfilePictureUrl,
    string? Bio,
    string Status,
    DateTime DateJoined = default);
