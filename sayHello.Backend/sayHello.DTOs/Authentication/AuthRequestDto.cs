namespace sayHello.DTOs.Authentication;

public record AuthRequestDto(
    string email,
    string password
    );