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
        private readonly UserValidator _validator;

        public UserService(
            AppDbContext context,
            ILogger<UserService> logger,
            IMapper mapper,
            UserValidator validator)
            : base(context, logger, mapper)
        {
            _validator = validator;
        }

        public async Task<UserDetailsDto> AddUserAsync(CreateUserDto createUserDto)
        {
            try
            {
                var userEntity = _mapper.Map<User>(createUserDto);

                var validationResult = await _validator.ValidateAsync(userEntity);
                if (!validationResult.IsValid)
                {
                    _logger.LogError("Validation failed: {Errors}",
                        string.Join(", ", validationResult.Errors.Select(e => e.ErrorMessage)));
                    throw new ValidationException(validationResult.Errors);
                }

                return await CreateAsync(_mapper.Map<UserDetailsDto>(userEntity));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error adding new user");
                throw;
            }
        }

        public override async Task UpdateAsync(UserDetailsDto userDetailsDto)
        {
            try
            {
                var existingUser = await _dbSet.FindAsync(userDetailsDto.UserId);
                if (existingUser == null)
                {
                    _logger.LogWarning("User not found: {UserId}", userDetailsDto.UserId);
                    throw new KeyNotFoundException($"User with ID {userDetailsDto.UserId} not found.");
                }

                _mapper.Map(userDetailsDto, existingUser);

                var validationResult = await _validator.ValidateAsync(existingUser);
                if (!validationResult.IsValid)
                {
                    _logger.LogError("Validation failed during update: {Errors}",
                        string.Join(", ", validationResult.Errors.Select(e => e.ErrorMessage)));
                    throw new ValidationException(validationResult.Errors);
                }

                _dbSet.Update(existingUser);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating user: {UserId}", userDetailsDto.UserId);
                throw;
            }
        }

        public async Task<UserDetailsDto?> GetUserByIdAsync(int id)
        {
            return await GetByIdAsync(id);
        }

        public async Task<IEnumerable<UserDetailsDto>> GetAllUsersAsync()
        {
            return await GetAllAsync();
        }

        public async Task SoftDeleteUserAsync(int userId)
        {
            try
            {
                var user = await _dbSet.FindAsync(userId);
                if (user == null)
                {
                    _logger.LogWarning("User not found for soft delete: {UserId}", userId);
                    throw new KeyNotFoundException($"User with ID {userId} not found.");
                }

                await SoftDeleteAsync(userId, "IsDeleted");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error soft deleting user: {UserId}", userId);
                throw;
            }
        }

        public async Task HardDeleteUserAsync(int userId)
        {
            try
            {
                var user = await _dbSet.FindAsync(userId);
                if (user == null)
                {
                    _logger.LogWarning("User not found for hard delete: {UserId}", userId);
                    throw new KeyNotFoundException($"User with ID {userId} not found.");
                }

                await HardDeleteAsync(userId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error hard deleting user: {UserId}", userId);
                throw;
            }
        }

        public async Task<bool> UserExistsAsync(int userId)
        {
            return await ExistsAsync(userId);
        }

        public async Task<IEnumerable<UserDetailsDto>> GetActiveUsersAsync()
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
        }
    }
}