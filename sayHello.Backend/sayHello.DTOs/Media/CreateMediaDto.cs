namespace sayHello.DTOs.Media;

public record CreateMediaDto(
    int MessageId,
    string FilePath,
    string MediaType
);