namespace sayHello.DTOs.Message;
  
public class MessageDetailsWithUsersInfoDto
{
    public int MessageId { get; set; }
    public string Content{ get; set; }
    public DateTime SendDT{ get; set; }
    public DateTime? ReadDT{ get; set; }
    public string ReadStatus{ get; set; }
    public int SenderId{ get; set; }
    public int? ReceiverId{ get; set; }
    public int? GroupId{ get; set; }
    public string? SenderName{ get; set; }
    public string? ReceiverName{ get; set; }
}
