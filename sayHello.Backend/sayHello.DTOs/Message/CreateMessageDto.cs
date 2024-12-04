namespace sayHello.DTOs.Message;

public record CreateMessageDto(
    string Content,
    int SenderId,
    int? ReceiverId,
    DateTime SendDT = default
);