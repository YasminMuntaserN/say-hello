using AutoMapper;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using sayHello.Business.Base;
using sayHello.DataAccess;
using sayHello.DTOs.BlockedUser;
using sayHello.Entities;
using sayHello.Validation;

namespace sayHello.Business
{
    public class BlockedUserService : BaseService<BlockedUser,BlockedUserDetailsDto>
    {
        private readonly IMapper _mapper; 
        
        public BlockedUserService(
            AppDbContext context,
            ILogger<BlockedUserService> logger,
            IMapper mapper, BlockedUserValidator validator)
            : base(context, logger, mapper ,validator)
        {
            _mapper = mapper;
        }

        public async Task<BlockedUserDetailsDto> AddBlockedUserAsync(CreateBlockedUserDto createBlockedUserDto)
            => await AddAsync(createBlockedUserDto, "BlockedUser");

        public async Task<BlockedUserDetailsDto?> UpdateBlockedUserAsync(int id, BlockedUserDetailsDto BlockedUserDetailsDto)
            => await UpdateAsync(id, BlockedUserDetailsDto, "BlockedUser");

        public async Task<BlockedUserDetailsDto?> GetBlockedUserByIdAsync(int id)
            => await FindBy(e => EF.Property<int>(e, "BlockedUserId") == id);
   
        public async Task<IEnumerable<BlockedUserDetailsDto>> GetAllBlockedUsersAsync()
            => await GetAllAsync();

        public async Task<bool> SoftDeleteBlockedUserAsync(int BlockedUserId)
            => await SoftDeleteAsync(BlockedUserId, "IsBlocked");

        public async Task<bool> HardDeleteBlockedUserAsync(int BlockedUserId)
            => await HardDeleteAsync(BlockedUserId, "BlockedUserId");

        public async Task<bool> BlockedUserExistsAsync(int BlockedUserId)
            => await ExistsAsync(BlockedUserId);
      

    }
}