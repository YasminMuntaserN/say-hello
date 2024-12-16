using System.Net;
using FluentValidation;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using sayHello.api.Controllers.Base;
using sayHello.Business;
using sayHello.DTOs.Message;

namespace sayHello.api.Controllers;

[Route("Messages")]
[ApiController]
public class MessagesController : BaseController
{
    private readonly MessageService _MessageService;

    public MessagesController(MessageService messageService, ILogger<MessagesController> logger)
        : base(logger)
    {
        _MessageService = messageService;
    }

    [HttpGet("allBySenderId/{senderId:int}", Name = "GetAllMessagesBySenderId")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<IEnumerable<ConversationDetailsDto>>> GetAllMessagesBySenderId(int senderId)
        => await HandleResponse(()=>_MessageService.GetAllMessagesBySenderIdAsync(senderId), "Messages retrieved successfully");

    [HttpGet("all", Name = "GetAllMessages")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<IEnumerable<MessageDetailsDto>>> GetAllMessages()
        => await HandleResponse(()=>_MessageService.GetAllMessagesAsync(), "Messages retrieved successfully");

    
    [HttpGet("findMessageByMessageId/{id:int}", Name = "FindMessageByMessageId")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<MessageDetailsDto?>> FindMessageByMessageId(int id)
        => await HandleResponse(()=>_MessageService.GetMessageByIdAsync(id), "Message retrieved successfully");
    
  
    
    [HttpPost("", Name = "CreateMessage")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<MessageDetailsDto?>> Add([FromBody] CreateMessageDto newMessageDto)
        => await HandleResponse(()=>_MessageService.AddMessageAsync(newMessageDto), "Message creating  successfully");
    
    
    [HttpPut("updateMessage/{id:int}", Name = "UpdateMessage")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<MessageDetailsDto?>> Update([FromRoute] int id, [FromBody] MessageDetailsDto updatedMessageDto) 
        => await HandleResponse(()=>_MessageService.UpdateMessageAsync(id,updatedMessageDto), "Message Updating  successfully");
   
    [HttpPut("SeenMessage/{id:int}", Name = "SeenMessage")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<bool>> MakeTheMessageSeen([FromRoute] int id) 
        => await HandleResponse(()=>_MessageService.MakeTheMessageSeenAsync(id), "Message Seen  successfully");
    
    [HttpDelete("deleteMessage/{id:int}", Name = "HardDeleteMessage")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<bool>> HardDeleteMessage([FromRoute] int id)
        => await HandleResponse(()=>_MessageService.HardDeleteMessageAsync(id), "Message deleting  successfully");
 
    
    [HttpGet("MessageExists/{id:int}", Name = "MessageExists")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<bool>> MessageExistsAsync([FromRoute] int id)
        => await HandleResponse(()=>_MessageService.MessageExistsAsync(id), "Message Founded  successfully");
    
    
}