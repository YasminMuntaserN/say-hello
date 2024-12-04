namespace sayHello.Entities;

public class Media
{
    public int MediaId { get; set; }
   
    public int MessageId { get; set; }
    public Message Message { get; set; }
    
    public string? FilePath { get; set; }
    public string MediaType { get; set; }
}
