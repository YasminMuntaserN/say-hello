namespace sayHello.DTOs.Media;

public record MediaDetailsDto(
    int MediaId,
    int MessageId,
    string FilePath,
    string MediaType
);