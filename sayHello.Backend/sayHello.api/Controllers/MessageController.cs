using System.Net;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using sayHello.api.Authorization;
using sayHello.api.Controllers.Base;
using sayHello.Business;
using sayHello.Business.Services;
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
    [RequirePermission(Permissions.ViewChats)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<IEnumerable<ConversationDetailsDto>>> GetAllMessagesBySenderId(int senderId)
        => await HandleResponse(() => _MessageService.GetAllMessagesBySenderIdAsync(senderId), "Messages retrieved successfully");

    [HttpGet("all", Name = "GetAllMessages")]
    [RequirePermission(Permissions.View)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<IEnumerable<MessageDetailsDto>>> GetAllMessages()
        => await HandleResponse(() => _MessageService.GetAllMessagesAsync(), "Messages retrieved successfully");

    [HttpGet("all/{senderId:int}/{receiverId:int}", Name = "GetMessagesInChatRoom")]
    [RequirePermission(Permissions.ViewChats)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<IEnumerable<MessageDetailsWithUsersInfoDto>>> GetMessagesInChatRoom(int senderId, int receiverId)
        => await HandleResponse(() => _MessageService.GetMessagesInChatRoomAsync(senderId,receiverId), "Messages retrieved successfully");
    
   
    [HttpGet("all/{groupId:int}", Name = "GetMessagesInChatRoomForGroup")]
    [RequirePermission(Permissions.ViewChats)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<IEnumerable<MessageDetailsWithUsersInfoDto>>> GetMessagesInChatRoomForGroup(int groupId)
        => await HandleResponse(() => _MessageService.GetMessagesInChatRoomForGroupAsync(groupId), "Messages retrieved successfully");
    
    
    [HttpGet("findMessageByMessageId/{id:int}", Name = "FindMessageByMessageId")]
    [RequirePermission(Permissions.SendMessages)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<MessageDetailsDto?>> FindMessageByMessageId(int id)
        => await HandleResponse(() => _MessageService.GetMessageByIdAsync(id), "Message retrieved successfully");



    [HttpPost("", Name = "CreateMessage")]
    [RequirePermission(Permissions.SendMessages)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<MessageDetailsDto?>> Add([FromBody] CreateMessageDto newMessageDto)
    {
        try
        {
            var createdMessage = await _MessageService.AddMessageAsync(newMessageDto);

            //await _hubContext.Clients.User(createdMessage.ReceiverId.ToString())
            //    .SendAsync("ReceiveMessage", createdMessage.SenderId, createdMessage.Content);

            return Ok(new { message = "Message created successfully", data = createdMessage });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "Internal server error", error = ex.Message });
        }
    }


    [HttpPut("updateMessage/{id:int}", Name = "UpdateMessage")]
    [RequirePermission(Permissions.SendMessages)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<MessageDetailsDto?>> Update([FromRoute] int id, [FromBody] MessageDetailsDto updatedMessageDto)
        => await HandleResponse(() => _MessageService.UpdateMessageAsync(id, updatedMessageDto), "Message Updating  successfully");

    [HttpPut("SeenMessage/{id:int}", Name = "SeenMessage")]
    [RequirePermission(Permissions.SendMessages)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<bool>> MakeTheMessageSeen([FromRoute] int id)
        => await HandleResponse(() => _MessageService.MakeTheMessageSeenAsync(id), "Message Seen  successfully");

    [HttpDelete("deleteMessage/{id:int}", Name = "HardDeleteMessage")]
    [RequirePermission(Permissions.SendMessages)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<bool>> HardDeleteMessage([FromRoute] int id)
        => await HandleResponse(() => _MessageService.HardDeleteMessageAsync(id), "Message deleting  successfully");


    [HttpGet("MessageExists/{id:int}", Name = "MessageExists")]
    [RequirePermission(Permissions.SendMessages)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<bool>> MessageExistsAsync([FromRoute] int id)
        => await HandleResponse(() => _MessageService.MessageExistsAsync(id), "Message Founded  successfully");


}