namespace sayHello.DTOs.Message;

public record CreateMessageDto(
    string Content,
    int SenderId,
    string ReadStatus,
    int? ReceiverId,
    IEnumerable<string> MediaUrls,
    DateTime? SendDT = null
);