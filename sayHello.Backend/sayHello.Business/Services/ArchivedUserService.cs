using AutoMapper;
using FluentValidation;
using Microsoft.Extensions.Logging;
using sayHello.Business.Base;
using sayHello.DataAccess;
using sayHello.DTOs.ArchivedUser;
using sayHello.Entities;
using sayHello.Validation;

namespace sayHello.Business
{
    public class ArchivedUserService : BaseService<ArchivedUser, ArchivedUserDetailsDto>
    {
        private readonly ArchivedUserValidator _validator;

        public ArchivedUserService(
            AppDbContext context,
            ILogger<ArchivedUserService> logger,
            IMapper mapper,
            ArchivedUserValidator validator)
            : base(context, logger, mapper)
        {
            _validator = validator;
        }

     
        public async Task<ArchivedUserDetailsDto> AddArchivedUserAsync(CreateArchivedUserDto createArchivedUserDto)
        {
            try
            {
                var ArchivedUserEntity = _mapper.Map<ArchivedUser>(createArchivedUserDto);

                var validationResult = await _validator.ValidateAsync(ArchivedUserEntity);
                if (!validationResult.IsValid)
                {
                    _logger.LogError("Validation failed: {Errors}",
                        string.Join(", ", validationResult.Errors.Select(e => e.ErrorMessage)));
                    throw new ValidationException(validationResult.Errors);
                }

                return await CreateAsync(_mapper.Map<ArchivedUserDetailsDto>(ArchivedUserEntity));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error adding new ArchivedUser");
                throw;
            }
        }

       
        public override async Task UpdateAsync(ArchivedUserDetailsDto ArchivedUserDetailsDto)
        {
            try
            {
                var existingArchivedUser = await _dbSet.FindAsync(ArchivedUserDetailsDto.ArchivedUserId);
                if (existingArchivedUser == null)
                {
                    _logger.LogWarning("ArchivedUser not found: {ArchivedUserId}", ArchivedUserDetailsDto.ArchivedUserId);
                    throw new KeyNotFoundException($"ArchivedUser with ID {ArchivedUserDetailsDto.ArchivedUserId} not found.");
                }

                _mapper.Map(ArchivedUserDetailsDto, existingArchivedUser);

                var validationResult = await _validator.ValidateAsync(existingArchivedUser);
                if (!validationResult.IsValid)
                {
                    _logger.LogError("Validation failed during update: {Errors}",
                        string.Join(", ", validationResult.Errors.Select(e => e.ErrorMessage)));
                    throw new ValidationException(validationResult.Errors);
                }

                _dbSet.Update(existingArchivedUser);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating ArchivedUser: {ArchivedUserId}", ArchivedUserDetailsDto.ArchivedUserId);
                throw;
            }
        }

        public async Task<ArchivedUserDetailsDto?> GetArchivedUserByIdAsync(int id)
        {
            return await GetByIdAsync(id);
        }

    
        public async Task<IEnumerable<ArchivedUserDetailsDto>> GetAllArchivedUsersAsync()
        {
            return await GetAllAsync();
        }

  
        /*public async Task SoftDeleteArchivedUserAsync(int ArchivedUserId)
        {
            try
            {
                var ArchivedUser = await _dbSet.FindAsync(ArchivedUserId);
                if (ArchivedUser == null)
                {
                    _logger.LogWarning("ArchivedUser not found for soft delete: {ArchivedUserId}", ArchivedUserId);
                    throw new KeyNotFoundException($"ArchivedUser with ID {ArchivedUserId} not found.");
                }

                await SoftDeleteAsync(ArchivedUserId, "IsDeleted");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error soft deleting ArchivedUser: {ArchivedUserId}", ArchivedUserId);
                throw;
            }
        }*/

  
        public async Task HardDeleteArchivedUserAsync(int ArchivedUserId)
        {
            try
            {
                var ArchivedUser = await _dbSet.FindAsync(ArchivedUserId);
                if (ArchivedUser == null)
                {
                    _logger.LogWarning("ArchivedUser not found for hard delete: {ArchivedUserId}", ArchivedUserId);
                    throw new KeyNotFoundException($"ArchivedUser with ID {ArchivedUserId} not found.");
                }

                await HardDeleteAsync(ArchivedUserId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error hard deleting ArchivedUser: {ArchivedUserId}", ArchivedUserId);
                throw;
            }
        }

    
        public async Task<bool> ArchivedUserExistsAsync(int ArchivedUserId)
        {
            return await ExistsAsync(ArchivedUserId);
        }

    }
}