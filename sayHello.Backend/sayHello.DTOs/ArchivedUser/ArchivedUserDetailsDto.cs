namespace sayHello.DTOs.ArchivedUser;

public record ArchivedUserDetailsDto(
    int Id,
    DateTime DateArchived,
    int UserId,
    int ArchivedUserId
);