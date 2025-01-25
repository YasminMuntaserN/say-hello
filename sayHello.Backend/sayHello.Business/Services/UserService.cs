using AutoMapper;
using FluentValidation;
using Microsoft.Data.SqlClient;
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
        private readonly ILogger<UserService> _logger;


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
            _logger = logger;
        }

        public async Task<UserDetailsDto> AddUserAsync(CreateUserDto createUserDto)
            => await AddAsync(createUserDto, "User");

        public async Task<UserDetailsDto?> UpdateUserAsync(int id, UserDetailsDto updatedUserDto)
        {
            try
            {
                var user = await _dbSet.FindAsync(id);
                if (user == null)
                {
                    _logger.LogWarning("User not found: {Id}", id);
                    throw new KeyNotFoundException($"User with ID {id} not found.");
                }

                user.Username = updatedUserDto.Username;
                user.ProfilePictureUrl = updatedUserDto.ProfilePictureUrl;
                user.Password = updatedUserDto.Password;
                user.Bio = updatedUserDto.Bio;
                
                _context.Users.Update(user);
                await _context.SaveChangesAsync();

                return _mapper.Map<UserDetailsDto>(user);
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
            => await FindBy(e =>
                EF.Property<string>(e, "Email") == Email && EF.Property<string>(e, "Password") == Password);

        public async Task<UserDetailsDto?> GetUserByUserNameAsync(string Username)
            => await FindBy(e => EF.Property<string>(e, "Username") == Username);

        public async Task<UserDetailsDto?> GetUserByEmailAsync(string Email)
            => await FindBy(e => EF.Property<string>(e, "Email") == Email);

        public async Task<IEnumerable<UserDetailsDto>> GetAllUsersAsync()
            => await GetAllAsync();

        public async Task<bool> SoftDeleteUserAsync(int userId)
        {
            // => await SoftDeleteAsync(userId, "IsDeleted");

            try
            {
                var affectedRows = await _context.Database.ExecuteSqlRawAsync(
                    "UPDATE Users SET IsDeleted = 1 WHERE UserId = @userId",
                    new[] 
                    {
                        new SqlParameter("@userId", userId)
                    });

                return affectedRows > 0;
            }

            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while soft-deleting entity: {EntityId}", userId);
                throw;
            }
        }

        public async Task<bool> HardDeleteUserAsync(int userId)
            => await HardDeleteAsync(userId, "UserId");

        public async Task<bool> UserExistsAsync(int userId)
            => await ExistsAsync(userId);

        public async Task<bool> ChangePassword(int userId, string newPassword)
        {try
            {
                var affectedRows = await _context.Database.ExecuteSqlRawAsync(
                    "UPDATE Users SET Password = @newPassword WHERE UserId = @userId",
                    new[] 
                    {
                        new SqlParameter("@newPassword", newPassword),
                        new SqlParameter("@userId", userId)
                    });

                return affectedRows > 0;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating User: {UserId}", userId);
                throw; 
            }
        }
    }
}
