using System.Net;
using FluentValidation;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using sayHello.api.Controllers.Base;
using sayHello.Business;
using sayHello.DTOs.ArchivedUser;

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
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<int>> GetArchivedUsersCount(int id)
        => await HandleResponse(()=>_ArchivedUserService.ArchivedUsersCountAsync(id), "Archived Users count retrieved successfully");
    
    
    [HttpGet("all", Name = "GetAllArchivedArchivedUsers")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<IEnumerable<ArchivedUserDetailsDto>>> GetAllArchivedArchivedUsers()
        => await HandleResponse(()=>_ArchivedUserService.GetAllArchivedUsersAsync(), "ArchivedArchivedUsers retrieved successfully");



    [HttpGet("findArchivedId/{id:int}", Name = "FindArchivedUserById")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<ArchivedUserDetailsDto?>> FindArchivedUserByArchivedUserId(int id)
        => await HandleResponse(()=>_ArchivedUserService.GetArchivedUserByIdAsync(id), "ArchivedUser retrieved successfully");
    
  
    [HttpPost("", Name = "CreateArchivedUser")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<ArchivedUserDetailsDto?>> Add([FromBody] CreateArchivedUserDto newArchivedUserDto)
        => await HandleResponse(()=>_ArchivedUserService.AddArchivedUserAsync(newArchivedUserDto), "ArchivedUser creating  successfully");
    
    
    [HttpPut("updateArchivedUser/{id:int}", Name = "UpdateArchivedUser")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<ArchivedUserDetailsDto?>> Update([FromRoute] int id, [FromBody] ArchivedUserDetailsDto updatedArchivedUserDto) 
        => await HandleResponse(()=>_ArchivedUserService.UpdateArchivedUserAsync(id,updatedArchivedUserDto), "ArchivedUser Updating  successfully");
   

    [HttpDelete("{id:int}", Name = "SoftDeleteArchivedUser")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<bool>> SoftDeleteArchivedUser([FromRoute] int id)
        => await HandleResponse(()=>_ArchivedUserService.SoftDeleteArchivedUserAsync(id), "ArchivedUser deleting  successfully");
    
    
    [HttpDelete("deleteArchivedUser/{id:int}", Name = "HardDeleteArchivedUser")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<bool>> HardDeleteArchivedUser([FromRoute] int id)
        => await HandleResponse(()=>_ArchivedUserService.HardDeleteArchivedUserAsync(id), "ArchivedUser deleting  successfully");
 
    
    [HttpGet("ArchivedUserExists/{id:int}", Name = "ArchivedUserExists")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<bool>> ArchivedUserExistsAsync([FromRoute] int id)
        => await HandleResponse(()=>_ArchivedUserService.ArchivedUserExistsAsync(id), "ArchivedUser Founded  successfully");
    
    
}