namespace sayHello.DTOs.Message;

public class ConversationDetailsDto
{
    public int SenderId { get; set; }
    public string SenderName { get; set; }
    public int ReceiverId { get; set; }
    public string ReceiverName { get; set; }
    public string ReceiverImage { get; set; }
    public int UnReadMessagesCount { get; set; }
    public string LastMessage { get; set; }
    public string LastMessageStatus { get; set; }
    public DateTime? LastMessageTime { get; set; }
    public DateTime? LastLoginForReceiver { get; set; }
}