namespace sayHello.api.Authorization;

public enum Permissions
{
    View  = 1,
    ManageUsers =2,
    SendMessages =4,
    ManageGroups =8,
    BlockUsers =16 ,
    ArchiveChats =32,
    AddGroupMember =64 ,
    RemoveGroupMember =128,
    ViewChats =256, 
    AuthenticateUsers = 512 
}