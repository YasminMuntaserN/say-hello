namespace sayHello.DTOs.Message;
public record CreateMessageDto(
    string Content,
    int SenderId,
    string ReadStatus,
    int? ReceiverId,
    int? GroupId,
    DateTime? SendDT = default
);