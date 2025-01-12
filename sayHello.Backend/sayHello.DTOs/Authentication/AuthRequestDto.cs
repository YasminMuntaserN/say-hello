namespace sayHello.DTOs.Authentication;

public record AuthRequestDto(
    string username,
    string password
    );