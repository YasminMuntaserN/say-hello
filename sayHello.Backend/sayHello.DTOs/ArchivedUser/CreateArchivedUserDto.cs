namespace sayHello.DTOs.ArchivedUser;

public record CreateArchivedUserDto(
    int UserId,
    int ArchivedUserId,
    DateTime DateArchived
);