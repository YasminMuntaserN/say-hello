namespace sayHello.Entities;

public class Message
{
    public int MessageId { get; set; }
    public string Content { get; set; }
    public DateTime SendDT { get; set; }
    public DateTime ReadDT { get; set; }
    public string ReadStatus{ get; set; }//Read ,  UnRead
  
    public int SenderId { get; set; }
    public User Sender { get; set; } 
    
    public int? ReceiverId { get; set; }
    public User Receiver { get; set; } 
    
    public ICollection<Media> Medias { get; set; }=new List<Media>();
}