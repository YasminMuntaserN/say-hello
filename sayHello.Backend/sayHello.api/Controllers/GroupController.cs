using System.Net;
using FluentValidation;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using sayHello.api.Controllers.Base;
using sayHello.Business;
using sayHello.DTOs.Group;
using sayHello.DTOs.Group;

namespace sayHello.api.Controllers;

[Route("Group")]
[ApiController]
public class GroupController : BaseController
{
    private readonly GroupService _GroupService;

    public GroupController(GroupService GroupService, ILogger<GroupController> logger)
        : base(logger)
    {
        _GroupService = GroupService;
    }

    [HttpGet("all", Name = "GetAllGroup")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<IEnumerable<GroupDetailsDto>>> GetAllGroup()
        => await HandleResponse(()=>_GroupService.GetAllGroupsAsync(), "Group retrieved successfully");



    [HttpGet("findGroupByGroupId/{id:int}", Name = "FindGroupByGroupId")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<GroupDetailsDto?>> FindGroupByGroupId(int id)
        => await HandleResponse(()=>_GroupService.GetGroupByIdAsync(id), "Group retrieved successfully");



    [HttpPost("", Name = "CreateGroup")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<GroupDetailsDto>> CreateGroup([FromForm] CreateGroupDto newGroupDto)
    => await HandleResponse(() => _GroupService.AddGroupAsync(newGroupDto));

    
    [HttpDelete("deleteGroup/{id:int}", Name = "HardDeleteGroup")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<bool>> HardDeleteGroup([FromRoute] int id)
        => await HandleResponse(()=>_GroupService.HardDeleteGroupAsync(id), "Group deleting  successfully");
 
    
    [HttpGet("GroupExists/{id:int}", Name = "GroupExists")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<bool>> GroupExistsAsync([FromRoute] int id)
        => await HandleResponse(()=>_GroupService.GroupExistsAsync(id), "Group Founded  successfully");
    
    
}