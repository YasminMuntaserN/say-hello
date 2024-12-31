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

    
    [HttpGet("all/{id:int}", Name = "GetAllGroupsContainingUser")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<IEnumerable<GroupConversationDetailsDto>>> GetAllGroupsContainingUser([FromRoute] int id)
        => await HandleResponse(()=>_GroupService.GetAllGroupsContainingUserAsync(id), "All Groups Containing User retrieved successfully");


    [HttpGet("findGroupByGroupId/{id:int}", Name = "FindGroupByGroupId")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<GroupDetailsDto?>> FindGroupByGroupId([FromRoute] int id)
        => await HandleResponse(()=>_GroupService.GetGroupByIdAsync(id), "Group retrieved successfully");



    [HttpPost("", Name = "CreateGroup")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<GroupDetailsDto>> CreateGroup([FromForm] CreateGroupDto newGroupDto, IFormFile? photo)
    {
        try
        {
            string ImageUrl = "/uploads/users/757ced0f-2fe3-4ad7-a911-187472cf51e8_group.jpg"; 

            if (photo != null && photo.Length > 0)
            {
                ImageUrl = await HelperClass.SaveImageAsync(photo, "users");

                if (ImageUrl == null)
                    return BadRequest("Failed to save the image.");
            }

            newGroupDto.ImageUrl = ImageUrl;

            Console.WriteLine(newGroupDto.ImageUrl);
            var result = await _GroupService.AddGroupAsync(newGroupDto);

            if (result == null)
            {
                _logger.LogWarning("HandleResponse: No data found.");
                return NotFound(new { message = "No data found." });
            }

            return Ok(result);
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