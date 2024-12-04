namespace sayHello.DTOs.User;

public record UserDetailsDto(
    int Id,
    string Username,
    string Email,
    string Password,
    string ProfilePictureUrl,
    string Bio,
    string Status,
    DateTime DateJoined = default);
