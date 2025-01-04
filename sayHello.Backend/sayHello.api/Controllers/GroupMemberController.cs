using System.Net;
using FluentValidation;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using sayHello.api.Controllers.Base;
using sayHello.Business;
using sayHello.DTOs.Group;

namespace sayHello.api.Controllers;

[Route("GroupMember")]
[ApiController]
public class GroupMemberController : BaseController
{
    private readonly GroupMemberService _GroupMemberService;

    public GroupMemberController(GroupMemberService GroupMemberService, ILogger<GroupMemberController> logger)
        : base(logger)
    {
        _GroupMemberService = GroupMemberService;
    }

    [HttpGet("all", Name = "GetAllGroupMember")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<IEnumerable<GroupDetailsMemberDto>>> GetAllGroupMember()
        => await HandleResponse(() => _GroupMemberService.GetAllGroupMembersAsync(), "GroupMember retrieved successfully");

    [HttpGet("all/{GroupId:int}", Name = "GetAllGroupMemberById")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<IEnumerable<GroupDetailsMemberDto>>> GetAllGroupMember(int GroupId)
       => await HandleResponse(() => _GroupMemberService.GetAllGroupMembersAsync(GroupId), "GroupMember retrieved successfully");


    [HttpPost("", Name = "CreateGroupMember")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<GroupDetailsMemberDto>> CreateGroupMember([FromForm] CreateGroupMemberDto newGroupMemberDto)
    => await HandleResponse(() => _GroupMemberService.AddGroupMemberAsync(newGroupMemberDto));
    
    [HttpGet("countGroupMembers", Name = "GetGroupMembersCount")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<int>> GroupMembersCount(int GroupId)
        => await HandleResponse(()=>_GroupMemberService.GroupMembersCountAsync(GroupId), "Group members count retrieved successfully");
    
    [HttpDelete("deleteGroupMember/{id:int}", Name = "HardDeleteGroupMember")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<bool>> HardDeleteGroupMember([FromRoute] int id)
        => await HandleResponse(()=>_GroupMemberService.HardDeleteGroupMemberAsync(id), "Group member deleting  successfully");
    
    [HttpDelete("deleteGroupMemberByUserId/{id:int}", Name = "HardDeleteGroupMemberByUserId")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<bool>> HardDeleteGroupMemberByUserId([FromRoute] int UsreId)
        => await HandleResponse(()=>_GroupMemberService.HardDeleteGroupMemberByUserIdAsync(UsreId), "Group member deleting  successfully");
    
    [HttpGet("count", Name = "GetAllGroupsContainingUserCount")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<int>> GetAllGroupsContainingUserCount(int id)
        => await HandleResponse(()=>_GroupMemberService.GetAllGroupsContainingUserCountAsync(id), "Groups count retrieved successfully");

}
