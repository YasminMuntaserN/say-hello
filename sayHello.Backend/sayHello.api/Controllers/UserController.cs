using System.Net;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using sayHello.api.Controllers.Base;
using sayHello.Business;
using sayHello.DTOs.User;

namespace sayHello.api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController : BaseController
{
    private readonly UserService _userService;
    private readonly ILogger<UserController> _logger;

    public UserController(
        UserService userService,
        ILogger<UserController> logger)
    {
        _userService = userService;
        _logger = logger;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllUsers()
    {
        try
        {
            var users = await _userService.GetAllUsersAsync();
            return HandleResponse(users, "Users retrieved successfully");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving users");
            return HandleError("Error retrieving users", HttpStatusCode.InternalServerError);
        }
    }

    [HttpGet("active")]
    public async Task<IActionResult> GetActiveUsers()
    {
        try
        {
            var users = await _userService.GetActiveUsersAsync();
            return HandleResponse(users, "Active users retrieved successfully");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving active users");
            return HandleError("Error retrieving active users", HttpStatusCode.InternalServerError);
        }
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetUserById(int id)
    {
        try
        {
            var user = await _userService.GetUserByIdAsync(id);
            if (user == null)
            {
                return HandleError("User not found", HttpStatusCode.NotFound);
            }

            return HandleResponse(user, "User retrieved successfully");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving user with ID: {UserId}", id);
            return HandleError("Error retrieving user", HttpStatusCode.InternalServerError);
        }
    }

    [HttpPost]
    public async Task<IActionResult> CreateUser([FromBody] CreateUserDto createUserDto)
    {
        try
        {
            var user = await _userService.AddUserAsync(createUserDto);
            return HandleResponse(user, "User created successfully");
        }
        catch (ValidationException ex)
        {
            _logger.LogWarning("Validation failed: {Errors}",
                string.Join(", ", ex.Errors.Select(e => e.ErrorMessage)));
            return HandleError("Validation failed", HttpStatusCode.BadRequest);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating user");
            return HandleError("Error creating user", HttpStatusCode.InternalServerError);
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateUser(int id, [FromBody] UserDetailsDto userDetailsDto)
    {
        try
        {
            if (id != userDetailsDto.UserId)
            {
                return HandleError("ID mismatch", HttpStatusCode.BadRequest);
            }

            await _userService.UpdateAsync(userDetailsDto);
            return HandleResponse(true, "User updated successfully");
        }
        catch (KeyNotFoundException ex)
        {
            _logger.LogWarning(ex.Message);
            return HandleError(ex.Message, HttpStatusCode.NotFound);
        }
        catch (ValidationException ex)
        {
            _logger.LogWarning("Validation failed: {Errors}",
                string.Join(", ", ex.Errors.Select(e => e.ErrorMessage)));
            return HandleError("Validation failed", HttpStatusCode.BadRequest);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating user with ID: {UserId}", id);
            return HandleError("Error updating user", HttpStatusCode.InternalServerError);
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteUser(int id)
    {
        try
        {
            await _userService.SoftDeleteUserAsync(id);
            return HandleResponse(true, "User deleted successfully");
        }
        catch (KeyNotFoundException ex)
        {
            _logger.LogWarning(ex.Message);
            return HandleError(ex.Message, HttpStatusCode.NotFound);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error deleting user with ID: {UserId}", id);
            return HandleError("Error deleting user", HttpStatusCode.InternalServerError);
        }
    }

    [HttpDelete("hard/{id}")]
    public async Task<IActionResult> HardDeleteUser(int id)
    {
        try
        {
            await _userService.HardDeleteUserAsync(id);
            return HandleResponse(true, "User permanently deleted successfully");
        }
        catch (KeyNotFoundException ex)
        {
            _logger.LogWarning(ex.Message);
            return HandleError(ex.Message, HttpStatusCode.NotFound);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error permanently deleting user with ID: {UserId}", id);
            return HandleError("Error permanently deleting user", HttpStatusCode.InternalServerError);
        }
    }
}
