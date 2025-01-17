using System.Net;
using FluentValidation;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using sayHello.api.Authorization;
using sayHello.api.Controllers.Base;
using sayHello.Business;
using sayHello.DTOs.ArchivedUser;
using sayHello.DTOs.User;
using sayHello.Entities;

namespace sayHello.api.Controllers;

[Route("ArchivedUsers")]
[ApiController]
public class ArchivedUsersController : BaseController
{
    private readonly ArchivedUserService _ArchivedUserService;

    public ArchivedUsersController(ArchivedUserService ArchivedUserService, ILogger<ArchivedUsersController> logger)
        : base(logger)
    {
        _ArchivedUserService = ArchivedUserService;
    }

    [HttpGet("count", Name = "GetArchivedUsersCount")]
    [RequirePermission(Permissions.ArchiveChats)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<int>> GetArchivedUsersCount(int id)
        => await HandleResponse(()=>_ArchivedUserService.ArchivedUsersCountAsync(id), "Archived Users count retrieved successfully");
    
    
    [HttpGet("all", Name = "GetAllArchivedUsers")]
    [RequirePermission(Permissions.View)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<IEnumerable<ArchivedUserDetailsDto>>> GetAllArchivedArchivedUsers()
        => await HandleResponse(()=>_ArchivedUserService.GetAllArchivedUsersAsync(), "ArchivedArchivedUsers retrieved successfully");



    [HttpGet("all/{id:int}", Name = "getAllArchivedUserById")]
    [RequirePermission(Permissions.ArchiveChats)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<IEnumerable<UserDetailsDto>>> FindArchivedUserByArchivedUserId(int id)
        => await HandleResponse(()=>_ArchivedUserService.GetAllArchivedUsersByUserIdAsync(id), "ArchivedUser retrieved successfully");
    
  
    [HttpPost("", Name = "CreateArchivedUser")]
    [RequirePermission(Permissions.ManageUsers)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<ArchivedUserDetailsDto?>> Add([FromBody] CreateArchivedUserDto newArchivedUserDto)
        => await HandleResponse(()=>_ArchivedUserService.AddArchivedUserAsync(newArchivedUserDto), "ArchivedUser creating  successfully");
    
    
    [HttpPut("updateArchivedUser/{id:int}", Name = "UpdateArchivedUser")]
    [RequirePermission(Permissions.ManageUsers)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<ArchivedUserDetailsDto?>> Update([FromRoute] int id, [FromBody] ArchivedUserDetailsDto updatedArchivedUserDto) 
        => await HandleResponse(()=>_ArchivedUserService.UpdateArchivedUserAsync(id,updatedArchivedUserDto), "ArchivedUser Updating  successfully");
   

    [HttpDelete("{id:int}", Name = "SoftDeleteArchivedUser")]
    [RequirePermission(Permissions.ManageUsers)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<bool>> SoftDeleteArchivedUser([FromRoute] int id)
        => await HandleResponse(()=>_ArchivedUserService.SoftDeleteArchivedUserAsync(id), "ArchivedUser deleting  successfully");
    
    
    [HttpDelete("deleteArchivedUser/{id:int}", Name = "HardDeleteArchivedUser")]
    [RequirePermission(Permissions.ManageUsers)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<bool>> HardDeleteArchivedUser([FromRoute] int id)
        => await HandleResponse(()=>_ArchivedUserService.HardDeleteArchivedUserAsync(id), "ArchivedUser deleting  successfully");
 
   
    [HttpDelete("deleteArchivedUser/{archivedUserId:int}/{archivedByUserId:int}", Name = "DeleteArchivedUserByArchivedUserIdArchivedByUserId")]
    [RequirePermission(Permissions.ArchiveChats)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<bool>> HardDeleteArchivedUser([FromRoute] int archivedUserId, int archivedByUserId)
        => await HandleResponse(()=>_ArchivedUserService.HardDeleteArchivedUserAsync(archivedUserId,archivedByUserId), "ArchivedUser deleting  successfully");
    
    
    [HttpGet("ArchivedUserExists/{id:int}", Name = "ArchivedUserExists")]
    [RequirePermission(Permissions.ArchiveChats)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<bool>> ArchivedUserExists([FromRoute] int id)
        => await HandleResponse(()=>_ArchivedUserService.ArchivedUserExistsAsync(id), "ArchivedUser Founded  successfully");
    
    [HttpGet("ArchivedUserExists/{archivedUserId:int}/{archivedByUserId:int}", Name = "ExistsByArchivedUserIdAndArchivedByUserId")]
    [RequirePermission(Permissions.ArchiveChats)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<bool>> ArchivedUserExists([FromRoute] int archivedUserId, int archivedByUserId)
        => await HandleResponse(()=>_ArchivedUserService.ArchivedUserExistsAsync(archivedUserId ,archivedByUserId), "ArchivedUser Founded  successfully");
    
    
}