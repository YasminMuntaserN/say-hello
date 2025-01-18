using System.Net;
using FluentValidation;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using sayHello.api.Authorization;
using sayHello.api.Controllers.Base;
using sayHello.Business;
using sayHello.DTOs.BlockedUser;
using sayHello.DTOs.User;
using sayHello.Entities;

namespace sayHello.api.Controllers;

[Route("BlockedUsers")]
[ApiController]
public class BlockedUsersController : BaseController
{
    private readonly BlockedUserService _BlockedUserService;

    public BlockedUsersController(BlockedUserService BlockedUserService, ILogger<BlockedUsersController> logger)
        : base(logger)
    {
        _BlockedUserService = BlockedUserService;
    }

    [HttpGet("count", Name = "GetBlockedUsersCount")]
    [RequirePermission(Permissions.BlockUsers)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<int>> GetBlockedUsersCount(int id)
        => await HandleResponse(()=>_BlockedUserService.BlockedUsersCountAsync(id), "Blocked Users count retrieved successfully");
    
    
    [HttpGet("all", Name = "GetAllBlockedUsers")]
    [RequirePermission(Permissions.View)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<IEnumerable<BlockedUserDetailsDto>>> GetAllBlockedBlockedUsers()
        => await HandleResponse(()=>_BlockedUserService.GetAllBlockedUsersAsync(), "BlockedBlockedUsers retrieved successfully");

    [HttpGet("all/{id:int}", Name = "GetAllBlockedUsersByUserId")]
    [RequirePermission(Permissions.BlockUsers)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<IEnumerable<UserDetailsDto>>> GetAllBlockedUsersByUserId(int id)
        => await HandleResponse(()=>_BlockedUserService.GetAllBlockedByUsersByUserIdAsync(id), "BlockedBlockedUsers retrieved successfully");

    
    [HttpPost("", Name = "CreateBlockedUser")]
    [RequirePermission(Permissions.BlockUsers)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<BlockedUserDetailsDto?>> Add([FromBody] CreateBlockedUserDto newBlockedUserDto)
        => await HandleResponse(()=>_BlockedUserService.AddBlockedUserAsync(newBlockedUserDto), "BlockedUser creating  successfully");
    
    
    [HttpPut("updateBlockedUser/{id:int}", Name = "UpdateBlockedUser")]
    [RequirePermission(Permissions.ManageUsers)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<BlockedUserDetailsDto?>> Update([FromRoute] int id, [FromBody] BlockedUserDetailsDto updatedBlockedUserDto) 
        => await HandleResponse(()=>_BlockedUserService.UpdateBlockedUserAsync(id,updatedBlockedUserDto), "BlockedUser Updating  successfully");
   

    [HttpDelete("{id:int}", Name = "SoftDeleteBlockedUser")]
    [RequirePermission(Permissions.ManageUsers)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<bool>> SoftDeleteBlockedUser([FromRoute] int id)
        => await HandleResponse(()=>_BlockedUserService.SoftDeleteBlockedUserAsync(id), "BlockedUser deleting  successfully");
    
    
    [HttpDelete("deleteBlockedUser/{id:int}", Name = "HardDeleteBlockedUser")]
    [RequirePermission(Permissions.BlockUsers)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<bool>> HardDeleteBlockedUser([FromRoute] int id)
        => await HandleResponse(()=>_BlockedUserService.HardDeleteBlockedUserAsync(id), "BlockedUser deleting  successfully");
 
    
    [HttpDelete("deleteBlockedUser/{BlockedUserId:int}/{BlockedByUserId:int}", Name = "DeleteBlockedUserByBlockedUserIdAndBlockedByUserId")]
    [RequirePermission(Permissions.BlockUsers)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<bool>> HardDeleteBlockedUser([FromRoute] int BlockedUserId, int BlockedByUserId)
        => await HandleResponse(()=>_BlockedUserService.HardDeleteBlockedUserAsync(BlockedUserId,BlockedByUserId), "BlockedUser deleting  successfully");
    
    
    [HttpGet("BlockedUserExists/{id:int}", Name = "BlockedUserExists")]
    [RequirePermission(Permissions.BlockUsers)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<bool>> BlockedUserExistsAsync([FromRoute] int id)
        => await HandleResponse(()=>_BlockedUserService.BlockedUserExistsAsync(id), "BlockedUser Founded  successfully");
    
    [HttpGet("BlockedUserExists/{BlockedUserId:int}/{BlockedByUserId:int}", Name = "ExistsByBlockedUserIdAndBlockedByUserId")]
    [RequirePermission(Permissions.BlockUsers)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<bool>> BlockedUserExists([FromRoute] int BlockedUserId, int BlockedByUserId)
        => await HandleResponse(()=>_BlockedUserService.BlockedUserExistsAsync(BlockedUserId ,BlockedByUserId), "BlockedUser Founded  successfully");
}