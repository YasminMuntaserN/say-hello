using System.Net;
using FluentValidation;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using sayHello.api.Controllers.Base;
using sayHello.Business;
using sayHello.DTOs.Media;
using sayHello.DTOs.Media;

namespace sayHello.api.Controllers;

[Route("Medias")]
[ApiController]
public class MediasController : BaseController
{
    private readonly MediaService _MediaService;

    public MediasController(MediaService MediaService, ILogger<MediasController> logger)
        : base(logger)
    {
        _MediaService = MediaService;
    }

    [HttpGet("all", Name = "GetAllMedias")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<IEnumerable<MediaDetailsDto>>> GetAllMedias()
        => await HandleResponse(()=>_MediaService.GetAllMediasAsync(), "Medias retrieved successfully");



    [HttpGet("findMediaByMediaId/{id:int}", Name = "FindMediaByMediaId")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<MediaDetailsDto?>> FindMediaByMediaId(int id)
        => await HandleResponse(()=>_MediaService.GetMediaByIdAsync(id), "Media retrieved successfully");
    
  
    
    [HttpPost("", Name = "CreateMedia")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<MediaDetailsDto?>> Add([FromBody] CreateMediaDto newMediaDto)
        => await HandleResponse(()=>_MediaService.AddMediaAsync(newMediaDto), "Media creating  successfully");
    
    
    [HttpPut("updateMedia/{id:int}", Name = "UpdateMedia")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<MediaDetailsDto?>> Update([FromRoute] int id, [FromBody] MediaDetailsDto updatedMediaDto) 
        => await HandleResponse(()=>_MediaService.UpdateMediaAsync(id,updatedMediaDto), "Media Updating  successfully");
   
   
    [HttpDelete("deleteMedia/{id:int}", Name = "HardDeleteMedia")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<bool>> HardDeleteMedia([FromRoute] int id)
        => await HandleResponse(()=>_MediaService.HardDeleteMediaAsync(id), "Media deleting  successfully");
 
    
    [HttpGet("MediaExists/{id:int}", Name = "MediaExists")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<bool>> MediaExistsAsync([FromRoute] int id)
        => await HandleResponse(()=>_MediaService.MediaExistsAsync(id), "Media Founded  successfully");
    
    
}