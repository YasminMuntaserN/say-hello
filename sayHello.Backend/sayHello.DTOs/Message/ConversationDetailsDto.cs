namespace sayHello.DTOs.Message;

public class ConversationDetailsDto
{
    public int ChatPartnerId { get; set; } 
    public string ChatPartnerName { get; set; }
    public string ChatPartnerImage { get; set; }
    public int UnReadMessagesCount { get; set; }
    public string LastMessage { get; set; }
    public string LastMessageStatus { get; set; }
    public string? bio { get; set; }
    public int IsReceiver { get; set; }
    public DateTime? LastMessageTime { get; set; }
    public DateTime? LastLoginForReceiver { get; set; }
}