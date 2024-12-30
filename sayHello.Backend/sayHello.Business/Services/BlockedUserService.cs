using AutoMapper;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using sayHello.Business.Base;
using sayHello.DataAccess;
using sayHello.DTOs.BlockedUser;
using sayHello.DTOs.User;
using sayHello.Entities;
using sayHello.Validation;

namespace sayHello.Business
{
    public class BlockedUserService : BaseService<BlockedUser,BlockedUserDetailsDto>
    {
        private readonly IMapper _mapper;
        private readonly AppDbContext _context;
        private readonly DbSet<User> _dbSet;


        public BlockedUserService(
            AppDbContext context,
            ILogger<BlockedUserService> logger,
            IMapper mapper,
            BlockedUserValidator validator)
            : base(context, logger, mapper, validator)
        {
            _context = context;
            _dbSet = context.Set<User>();
            _mapper = mapper;
        }

        public async Task<int> BlockedUsersCountAsync(int userId)
            => _dbSet
                .Where(u => u.UserId == userId)
                .SelectMany(u => u.BlockedUsers.Select(a => a.BlockedByUser)).Count();       
        public async Task<BlockedUserDetailsDto> AddBlockedUserAsync(CreateBlockedUserDto createBlockedUserDto)
            => await AddAsync(createBlockedUserDto, "BlockedUser");

        public async Task<BlockedUserDetailsDto?> UpdateBlockedUserAsync(int id, BlockedUserDetailsDto BlockedUserDetailsDto)
            => await UpdateAsync(id, BlockedUserDetailsDto, "BlockedUser");

  
        public async Task<IEnumerable<BlockedUserDetailsDto>> GetAllBlockedUsersAsync()
            => await GetAllAsync();

        public async Task<bool> SoftDeleteBlockedUserAsync(int BlockedUserId)
            => await SoftDeleteAsync(BlockedUserId, "IsBlocked");

        public async Task<bool> HardDeleteBlockedUserAsync(int BlockedUserId)
            => await HardDeleteAsync(BlockedUserId, "BlockedUserId");

        public async Task<bool> BlockedUserExistsAsync(int BlockedUserId)
            => await ExistsAsync(BlockedUserId);
      
        public async Task<bool> BlockedUserExistsAsync(int BlockedUserId, int BlockedByUserId)
            => await ExistsByAsync(e => e.BlockedByUserId == BlockedUserId && e.UserId == BlockedByUserId);
        
        public async Task<bool> HardDeleteBlockedUserAsync(int BlockedUserId, int BlockedByUserId)
            => await HardDeleteAsync("Blocked User",e => e.BlockedByUserId == BlockedUserId && e.UserId == BlockedByUserId);

        public async Task<IEnumerable<UserDetailsDto>> GetAllBlockedByUsersByUserIdAsync(int userId)
        {
            var blockedByUsers =  await _dbSet
                .Where(u => u.UserId == userId)
                .SelectMany(u => u.BlockedUsers.Select(a => a.BlockedByUser))
                .ToListAsync();

            if (!blockedByUsers.Any())
                throw new KeyNotFoundException($"No users found who blocked user with ID {userId}.");

            return _mapper.Map<IEnumerable<UserDetailsDto>>(blockedByUsers);
        }
        
        
    }
}