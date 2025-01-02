namespace sayHello.DTOs.Message;
  
public record MessageDetailsDto(
    int MessageId,
    string Content,
    DateTime SendDT,
    DateTime? ReadDT,
    string ReadStatus,
    int SenderId,
    int? ReceiverId,
    int? GroupId
    );