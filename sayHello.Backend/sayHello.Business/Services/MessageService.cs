using AutoMapper;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using sayHello.Business.Base;
using sayHello.DataAccess;
using sayHello.DTOs.Message;
using sayHello.Entities;
using sayHello.Validation;

namespace sayHello.Business
{
    public class MessageService : BaseService<Message, MessageDetailsDto>
    {

        private readonly MessageValidator _validator;
        private readonly AppDbContext _context;
        private readonly ILogger<MessageService> _logger;
        private readonly IMapper _mapper;

        public MessageService(
            AppDbContext context,
            ILogger<MessageService> logger,
            IMapper mapper,
            MessageValidator validator)
            : base(context, logger, mapper, validator)
        {
            _mapper = mapper;
            _context = context;
            _logger = logger;
        }

        public async Task<MessageDetailsDto> AddMessageAsync(CreateMessageDto createMessageDto)
            => await AddAsync(createMessageDto, "Message");

        public async Task<MessageDetailsDto?> UpdateMessageAsync(int id, MessageDetailsDto MessageDetailsDto)
            => await UpdateAsync(id, MessageDetailsDto, "Message");

        public async Task<MessageDetailsDto?> GetMessageByIdAsync(int id)
            => await FindBy(e => EF.Property<int>(e, "MessageId") == id);

        public async Task<IEnumerable<MessageDetailsDto>> GetAllMessagesAsync()
            => await GetAllAsync();

        public async Task<bool> HardDeleteMessageAsync(int MessageId)
            => await HardDeleteAsync(MessageId, "MessageId");

        public async Task<bool> MessageExistsAsync(int MessageId)
            => await ExistsAsync(MessageId);

        public async Task<bool> MakeTheMessageSeenAsync(int id)
        {
            var message = await _context.Messages.FirstOrDefaultAsync(m => m.MessageId == id);
            if (message == null)
            {
                _logger.LogWarning($"Message with ID {id} not found.");
                return false;
            }

            _context.Entry(message).State = EntityState.Modified;
            _logger.LogInformation($"Marking message with ID {id} as read.");
            message.ReadStatus = "Read";
            message.ReadDT = DateTime.Now;

            try
            {
                await _context.SaveChangesAsync();
                _logger.LogInformation($"Message with ID {id} successfully updated.");
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error saving changes for message with ID {id}: {ex.Message}");
                return false;
            }
        }

        
        // this method will return the latest chats 
        public async Task<IEnumerable<ConversationDetailsDto>> GetAllMessagesBySenderIdAsync(int senderId)
        {
            try
            {
                //this the user received them so here the user will be the sender 
                var conversations = _context.ConversationDetails
                    .FromSqlInterpolated($"SELECT * FROM GetConversationDetails({senderId})")
                    .ToList();
                
                //this the user sent them so here the user will be the receiver
                var ReceivedConversation=_context.ConversationDetails
                    .FromSqlInterpolated($"SELECT * FROM GetReceivedConversationDetails({senderId})")
                    .ToList();
                
                return conversations.Concat(ReceivedConversation).OrderByDescending(x=>x.LastMessageTime).DistinctBy(x=>x.ChatPartnerId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving all Messages by Sender ID.");
                throw;
            }
        }

       
        // this method will  retrieving conversations between a sender and a receiver.
        public async Task<IEnumerable<MessageDetailsDto>> GetMessagesInChatRoomAsync(int senderId, int receiverId)
        {
            var messages = await _context.Messages
                .Where(m => 
                    (m.SenderId == senderId && m.ReceiverId == receiverId) || 
                    (m.SenderId == receiverId && m.ReceiverId == senderId))
                .OrderBy(m => m.SendDT) 
                .ToListAsync();
            
            return _mapper.Map<IEnumerable<MessageDetailsDto>>(messages);
        }
    }
}
