using System.Net;
using FluentValidation;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using sayHello.api.Controllers.Base;
using sayHello.Business;
using sayHello.DTOs.User;

namespace sayHello.api.Controllers;

[Route("Users")]
[ApiController]
public class UsersController : BaseController
{
    private readonly UserService _userService;

    public UsersController(UserService userService, ILogger<UsersController> logger)
        : base(logger)
    {
        _userService = userService;
    }

    [HttpGet("all", Name = "GetAllUsers")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<IEnumerable<UserDetailsDto>>> GetAllUsers()
        => await HandleResponse(()=>_userService.GetAllUsersAsync(), "Users retrieved successfully");



    [HttpGet("findUserByUserId/{id:int}", Name = "FindUserByUserId")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<UserDetailsDto?>> FindUserByUserId(int id)
        => await HandleResponse(()=>_userService.GetUserByIdAsync(id), "User retrieved successfully");
    
  
    [HttpGet("findByUserName/{UserName}", Name = "FindUserByUserName")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<UserDetailsDto?>> FindUserByUserName(string UserName)
        => await HandleResponse(() => _userService.GetUserByUserNameAsync(UserName), "User retrieved successfully");

    [HttpGet("findByEmailAndPassword/{Email}/{Password}", Name = "FindUserByEmailAndPassword")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<UserDetailsDto?>> FindUserByEmailAndPassword(string Email, string Password)
        => await HandleResponse(() => _userService.GetUserByEmailAndPasswordAsync(Email, Password), "User retrieved successfully");
    
    
    [HttpPost("", Name = "CreateUser")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<UserDetailsDto?>> Add([FromBody] CreateUserDto newUserDto)
        => await HandleResponse(()=>_userService.AddUserAsync(newUserDto), "User creating  successfully");
    
    
    [HttpPut("updateUser/{id:int}", Name = "UpdateUser")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<UserDetailsDto?>> Update([FromRoute] int id, [FromBody] UserDetailsDto updatedUserDto) 
        => await HandleResponse(()=>_userService.UpdateUserAsync(id,updatedUserDto), "User Updating  successfully");
   

    [HttpDelete("{id:int}", Name = "SoftDeleteUser")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<bool>> SoftDeleteUser([FromRoute] int id)
        => await HandleResponse(()=>_userService.SoftDeleteUserAsync(id), "User deleting  successfully");
    
    
    [HttpDelete("deleteUser/{id:int}", Name = "HardDeleteUser")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<bool>> HardDeleteUser([FromRoute] int id)
        => await HandleResponse(()=>_userService.HardDeleteUserAsync(id), "User deleting  successfully");
 
    
    [HttpGet("userExists/{id:int}", Name = "UserExists")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<bool>> UserExistsAsync([FromRoute] int id)
        => await HandleResponse(()=>_userService.UserExistsAsync(id), "User Founded  successfully");
    
    
}