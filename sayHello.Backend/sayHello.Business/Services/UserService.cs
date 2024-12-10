using AutoMapper;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using sayHello.Business.Base;
using sayHello.DataAccess;
using sayHello.DTOs.User;
using sayHello.Entities;
using sayHello.Validation;

namespace sayHello.Business
{
    public class UserService : BaseService<User, UserDetailsDto>
    {
        private readonly IMapper _mapper;

        public UserService(
            AppDbContext context,
            ILogger<UserService> logger,
            IMapper mapper,
            UserValidator validator)
            : base(context, logger, mapper, validator)
        {
            _mapper = mapper;
        }

        public async Task<UserDetailsDto> AddUserAsync(CreateUserDto createUserDto)
            => await AddAsync(createUserDto, "User");

        public async Task<UserDetailsDto?> UpdateUserAsync(int id, UserDetailsDto userDetailsDto)
            => await UpdateAsync(id, userDetailsDto, "User");

        public async Task<UserDetailsDto?> GetUserByIdAsync(int id)
            => await FindBy(e => EF.Property<int>(e, "UserId") == id);

        public async Task<UserDetailsDto?> GetUserByEmailAndPasswordAsync(string Email , string Password)
            => await FindBy(e => EF.Property<string>(e, "Email") == Email && EF.Property<string>(e, "Password") == Password);
        
        public async Task<UserDetailsDto?> GetUserByUserNameAsync(string Username)
            => await FindBy(e => EF.Property<string>(e, "Username") == Username);
        
        
        public async Task<IEnumerable<UserDetailsDto>> GetAllUsersAsync()
            => await GetAllAsync();

        public async Task<bool> SoftDeleteUserAsync(int userId)
            => await SoftDeleteAsync(userId, "IsDeleted");

        public async Task<bool> HardDeleteUserAsync(int userId)
            => await HardDeleteAsync(userId, "UserId");

        public async Task<bool> UserExistsAsync(int userId)
            => await ExistsAsync(userId);

      
        /*     public async Task<IEnumerable<UserDetailsDto>> GetActiveUsersAsync()
             {
                 try
                 {
                     var activeUsers = await _dbSet
                         .Where(u => !u.IsDeleted)
                         .ToListAsync();

                     return _mapper.Map<IEnumerable<UserDetailsDto>>(activeUsers);
                 }
                 catch (Exception ex)
                 {
                     _logger.LogError(ex, "Error retrieving active users");
                     throw;
                 }
             }*/
    }
}