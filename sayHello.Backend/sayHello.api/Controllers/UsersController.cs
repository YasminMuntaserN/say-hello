using System.Net;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using sayHello.api.Authorization;
using sayHello.api.Controllers.Base;
using sayHello.Business;
using sayHello.DTOs.User;

namespace sayHello.api.Controllers;

[Route("Users")]
[ApiController]
[Authorize] 
public class UsersController : BaseController
{
    private readonly UserService _userService;

    public UsersController(UserService userService, ILogger<UsersController> logger)
        : base(logger)
    {
        _userService = userService;
    }

    [HttpGet("all", Name = "GetAllUsers")]
    [RequirePermission(Permissions.ViewChats)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<IEnumerable<UserDetailsDto>>> GetAllUsers()
        => await HandleResponse(()=>_userService.GetAllUsersAsync(), "Users retrieved successfully");



    [HttpGet("findUserByUserId/{id:int}", Name = "FindUserByUserId")]
    [RequirePermission(Permissions.View)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<UserDetailsDto?>> FindUserByUserId(int id)
        => await HandleResponse(()=>_userService.GetUserByIdAsync(id), "User retrieved successfully");
    
  
    [HttpGet("findByUserName/{UserName}", Name = "FindUserByUserName")]
    [RequirePermission(Permissions.View)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<UserDetailsDto?>> FindUserByUserName(string UserName)
        => await HandleResponse(() => _userService.GetUserByUserNameAsync(UserName), "User retrieved successfully");

    
    [HttpGet("findByEmailAndPassword/{Email}/{Password}", Name = "FindUserByEmailAndPassword")]
    [RequirePermission(Permissions.AuthenticateUsers)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<UserDetailsDto?>> FindUserByEmailAndPassword(string Email, string Password)
        => await HandleResponse(() => _userService.GetUserByEmailAndPasswordAsync(Email, Password), "User retrieved successfully");

   
    [HttpPost("", Name = "CreateUser")]
    [AllowAnonymous] 
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<CreateUserDto>> CreateUser([FromForm] CreateUserDto newUserDto, IFormFile? photo)
    {
        try
        {
            string ImageUrl = "/uploads/users/78edb029-8743-4ca2-8195-5e49ee419cf3_User.png"; 

            if (photo != null && photo.Length > 0)
            {
                ImageUrl = await HelperClass.SaveImageAsync(photo, "users");

                if (ImageUrl == null)
                    return BadRequest("Failed to save the image.");
            }

            newUserDto.ProfilePictureUrl = ImageUrl;

            Console.WriteLine(newUserDto.ProfilePictureUrl);
            var result = await _userService.AddUserAsync(newUserDto);

            if (result == null)
            {
                _logger.LogWarning("HandleResponse: No data found.");
                return NotFound(new { message = "No data found." });
            }

            _logger.LogInformation("User created successfully");
            
            EmailService _emailSender = new EmailService();
            string token = Guid.NewGuid().ToString(); 
            string confirmationLink = $"http://localhost:5173/dashboard/{result.Username}";


            if (string.IsNullOrEmpty(token))
            {
                return BadRequest("Invalid confirmation link.");
            }

            _emailSender.SendConfirmationEmail(result.Email, confirmationLink);

            return Ok(new
            {
                message = "Confirmation email sent! and User Created Successfully",
                user = result
            });
        }
        
        catch (ValidationException ex)
        {
            _logger.LogWarning("Validation failed: {Errors}",
                string.Join(", ", ex.Errors.Select(e => e.ErrorMessage)));

            return BadRequest(new
            {
                message = "Validation failed.",
                errors = ex.Errors.Select(e => e.ErrorMessage)
            });
        }
    }

    
    
    [HttpPost("restorePassword/{Email}", Name = "RestorePassword")]
    [AllowAnonymous] 
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<UserDetailsDto?>> RestorePassword(string Email)
    {
        if (string.IsNullOrWhiteSpace(Email))
        {
            return BadRequest("Invalid email format.");
        }

        EmailService _emailSender = new EmailService();
        string token = Guid.NewGuid().ToString(); 

        var user = await _userService.GetUserByEmailAsync(Email);
        if (user == null)
        {
            return BadRequest("No such user exists with the email address");
        }

        string confirmationLink = $"http://localhost:5173/dashboard/{user.Username}";

        if (string.IsNullOrEmpty(token))
        {
            return BadRequest("Invalid confirmation link.");
        }

        await _emailSender.SendConfirmationEmail(Email, confirmationLink, true);

        return Ok(new
        {
            message = "Password restored successfully!",
            user
        });
    }


  
    
    [HttpPut("updateUser/{id:int}", Name = "UpdateUser")]
    [RequirePermission(Permissions.AuthenticateUsers)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<UserDetailsDto?>> Update(
        [FromRoute] int id,
        [FromForm] UserDetailsDto updatedUserDto,
        IFormFile? photo)
    {
        if (id < 1 || updatedUserDto == null)
            return BadRequest("Invalid User Data");

        var existingUser = await _userService.GetUserByIdAsync(id);
        if (existingUser == null)
            return NotFound($"User with ID {id} not found.");

        if (photo != null)
        {
            var imageUrl = await HelperClass.SaveImageAsync(photo, "users");
            if (imageUrl == null)
            {
                _logger.LogError("Failed to save image for user {Id}", id);
                return BadRequest(new { message = "Failed to save the image." });
            }
            updatedUserDto.ProfilePictureUrl = imageUrl;
        }
        else
        {
            updatedUserDto.ProfilePictureUrl = existingUser.ProfilePictureUrl;
        }

        var result = await _userService.UpdateUserAsync(id, updatedUserDto);
        if (result == null)
        {
            _logger.LogWarning("Update failed for User ID: {Id}", id);
            return NotFound(new { message = "No data found." });
        }

        _logger.LogInformation("User {Id} updated successfully.", id);
        return Ok(result);
    }

    
    [HttpPut("changePassword/{id:int}", Name = "changePassword")]
    [RequirePermission(Permissions.AuthenticateUsers)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<bool>> ChangePassword([FromRoute] int id,  string newPassword)
        => await HandleResponse(()=>_userService.ChangePassword(id,newPassword), "User Password changed successfully");

    
    [HttpDelete("{id:int}", Name = "SoftDeleteUser")]
    [RequirePermission(Permissions.AuthenticateUsers)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<bool>> SoftDeleteUser([FromRoute] int id)
        => await HandleResponse(()=>_userService.SoftDeleteUserAsync(id), "User deleting  successfully");
    
    
    [HttpDelete("deleteUser/{id:int}", Name = "HardDeleteUser")]
    [RequirePermission(Permissions.ManageUsers)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<bool>> HardDeleteUser([FromRoute] int id)
        => await HandleResponse(()=>_userService.HardDeleteUserAsync(id), "User deleting  successfully");
 
    
    [HttpGet("userExists/{id:int}", Name = "UserExists")]
    [RequirePermission(Permissions.View)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<bool>> UserExistsAsync([FromRoute] int id)
        => await HandleResponse(()=>_userService.UserExistsAsync(id), "User Founded  successfully");
    
    
}