using System.Net;
using FluentValidation;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using sayHello.api.Controllers.Base;
using sayHello.Business;
using sayHello.DTOs.BlockedUser;

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

    [HttpGet("all", Name = "GetAllBlockedBlockedUsers")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<IEnumerable<BlockedUserDetailsDto>>> GetAllBlockedBlockedUsers()
        => await HandleResponse(()=>_BlockedUserService.GetAllBlockedUsersAsync(), "BlockedBlockedUsers retrieved successfully");



    [HttpGet("findBlockedUserByBlockedUserId/{id:int}", Name = "FindBlockedUserByBlockedUserId")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<BlockedUserDetailsDto?>> FindBlockedUserByBlockedUserId(int id)
        => await HandleResponse(()=>_BlockedUserService.GetBlockedUserByIdAsync(id), "BlockedUser retrieved successfully");
    
  
    [HttpPost("", Name = "CreateBlockedUser")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<BlockedUserDetailsDto?>> Add([FromBody] CreateBlockedUserDto newBlockedUserDto)
        => await HandleResponse(()=>_BlockedUserService.AddBlockedUserAsync(newBlockedUserDto), "BlockedUser creating  successfully");
    
    
    [HttpPut("updateBlockedUser/{id:int}", Name = "UpdateBlockedUser")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<BlockedUserDetailsDto?>> Update([FromRoute] int id, [FromBody] BlockedUserDetailsDto updatedBlockedUserDto) 
        => await HandleResponse(()=>_BlockedUserService.UpdateBlockedUserAsync(id,updatedBlockedUserDto), "BlockedUser Updating  successfully");
   

    [HttpDelete("{id:int}", Name = "SoftDeleteBlockedUser")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<bool>> SoftDeleteBlockedUser([FromRoute] int id)
        => await HandleResponse(()=>_BlockedUserService.SoftDeleteBlockedUserAsync(id), "BlockedUser deleting  successfully");
    
    
    [HttpDelete("deleteBlockedUser/{id:int}", Name = "HardDeleteBlockedUser")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<bool>> HardDeleteBlockedUser([FromRoute] int id)
        => await HandleResponse(()=>_BlockedUserService.HardDeleteBlockedUserAsync(id), "BlockedUser deleting  successfully");
 
    
    [HttpGet("BlockedUserExists/{id:int}", Name = "BlockedUserExists")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<bool>> BlockedUserExistsAsync([FromRoute] int id)
        => await HandleResponse(()=>_BlockedUserService.BlockedUserExistsAsync(id), "BlockedUser Founded  successfully");
    
    
}