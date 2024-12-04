namespace sayHello.DTOs.User;

public record CreateUserDto(
    string Username,
    string Email,
    string Password,
    string ProfilePictureUrl,
    string Bio,
    string Status,
    DateTime DateJoined = default);
