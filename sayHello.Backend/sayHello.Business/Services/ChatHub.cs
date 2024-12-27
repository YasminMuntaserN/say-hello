using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using sayHello.DataAccess;
using sayHello.DTOs.Message;
using sayHello.Entities;
using System.Collections.Concurrent;
using System.Data;

namespace sayHello.Business.Services
{
    public class ChatHub : Hub
    {
        private readonly AppDbContext _context;
        private readonly ILogger<ChatHub> _logger;
        private readonly MessageService _MessageService;

        public ChatHub(AppDbContext context, ILogger<ChatHub> logger, MessageService messageService)
        {
            _context = context;
            _logger = logger;
            _MessageService = messageService;
        }

        public async Task JoinChatRoom(string userId, string chatRoom)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, chatRoom);
            await Clients.Group(chatRoom).SendAsync("UserJoinedRoom", userId);
        }

        public async Task SendMessage(string chatRoom, CreateMessageDto dto)
        {
            if (string.IsNullOrEmpty(chatRoom) || dto == null)
                throw new ArgumentException("Invalid chatRoom or messageDto.");

            try
            {
              
                var message = await _MessageService.AddMessageAsync(dto);
                await Clients.Group(chatRoom).SendAsync("ReceiveMessage", message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error sending message");
                throw;
            }
        }
    }
}