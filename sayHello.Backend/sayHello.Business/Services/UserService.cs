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
        private readonly AppDbContext _context;
        private readonly DbSet<User> _dbSet;
        private readonly  ILogger<UserService> _logger;


        public UserService(
            AppDbContext context,
            ILogger<UserService> logger,
            IMapper mapper,
            UserValidator validator)
            : base(context, logger, mapper, validator)
        {
            _context = context;
            _dbSet = context.Set<User>();
            _mapper = mapper;
            _logger= logger;
        }

        public async Task<UserDetailsDto> AddUserAsync(CreateUserDto createUserDto)
            => await AddAsync(createUserDto, "User");

        public async Task<UserDetailsDto?> UpdateUserAsync(int id, UserDetailsDto updatedUserDto)
        {
            try
            {
                var affectedRows = await _context.Database.ExecuteSqlRawAsync(
                    "UPDATE Users SET Username = {0}, ProfilePictureUrl = {1}, Password = {2}, Bio = {3} WHERE UserId = {4}",
                    updatedUserDto.Username, updatedUserDto.ProfilePictureUrl, updatedUserDto.Password, updatedUserDto.Bio, id);

                if (affectedRows > 0)
                {
                    var updatedUser = await _dbSet.FindAsync(id);
                    if (updatedUser == null)
                    {
                        _logger.LogWarning("User not found: {Id}", id);
                        throw new KeyNotFoundException($"User with ID {id} not found.");
                    }

                    return _mapper.Map<UserDetailsDto>(updatedUser);
                }
                else
                {
                    _logger.LogError("Error updating User: {Id}", id);
                    throw new InvalidOperationException("Update operation failed.");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating User: {Id}", id);
                throw;
            }
        }

        public async Task<UserDetailsDto?> GetUserByIdAsync(int id)
            => await FindBy(e => EF.Property<int>(e, "UserId") == id);

        public async Task<UserDetailsDto?> GetUserByEmailAndPasswordAsync(string Email, string Password)
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


        public async Task<bool> IsUserNameUniqueAsync(string UserName)
            => await _context.Users.AnyAsync(u => u.Username == UserName) == false;

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