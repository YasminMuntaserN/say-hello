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
            : base(context, logger, mapper ,validator)
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


    }
}