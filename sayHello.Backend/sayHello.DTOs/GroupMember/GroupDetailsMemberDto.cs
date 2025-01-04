namespace sayHello.DTOs.Group;

public class GroupDetailsMemberDto
{
    public int Id{ get; set; }
    public int groupId{ get; set; }
    public int userId{ get; set; }
    public string? userImg{ get; set; }
    public string? username{ get; set; }
    public string? bio{ get; set; }

}