using System.Linq.Expressions;
using AutoMapper;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using sayHello.DataAccess;
using sayHello.DTOs.User;

namespace sayHello.Business.Base;

public abstract class BaseService<TEntity, TDto> : IBaseService<TEntity, TDto> where TEntity : class where TDto : class
{
    private readonly AppDbContext _context;
    private readonly DbSet<TEntity> _dbSet;
    private readonly ILogger<BaseService<TEntity,TDto>> _logger;
    private readonly IMapper _mapper; 
    private readonly IValidator<TEntity> _validator;

    protected BaseService(
        AppDbContext context,
        ILogger<BaseService<TEntity, TDto>> logger,
        IMapper mapper,
        IValidator<TEntity> validator)
    {
        _context = context;
        _dbSet = context.Set<TEntity>();
        _logger = logger;
        _mapper = mapper;
        _validator = validator;
    }

    public async Task<TDto?> FindBy(Expression<Func<TEntity, bool>> predicate)
    {
        try
        {
            var entity = await _dbSet.FirstOrDefaultAsync(predicate);
            return entity != null ? _mapper.Map<TDto>(entity) : null;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving entity");
            throw;
        }
    }

    public  async Task<IEnumerable<TDto>> GetAllAsync()
    {
        try
        {
            var entities = await _dbSet.ToListAsync();
            return _mapper.Map<IEnumerable<TDto>>(entities);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving all entities");
            throw;
        }
    }

    public async Task<TDto> AddAsync<TCreateDto>(TCreateDto createDto, string entityName)
    {
        try
        {
            var entity = _mapper.Map<TEntity>(createDto);

            var validationResult = await _validator.ValidateAsync(entity);
            if (!validationResult.IsValid)
            {
                _logger.LogError("Validation failed while creating new {EntityName}: {Errors}",
                    entityName, string.Join(", ", validationResult.Errors.Select(e => e.ErrorMessage)));
                throw new ValidationException(validationResult.Errors);
            }

            await _dbSet.AddAsync(entity);
            await _context.SaveChangesAsync();

            return _mapper.Map<TDto>(entity);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating new {EntityName}", entityName);
            throw;
        }
    }

    public virtual async Task<TDto?> UpdateAsync(int id, TDto dto, string entityName ,bool isUpdate=false)
    {
        try
        {
            var existingEntity = await _dbSet.FindAsync(id);
            if (existingEntity == null)
            {
                _logger.LogWarning("{EntityName} not found: {Id}", entityName, id);
                throw new KeyNotFoundException($"{entityName} with ID {id} not found.");
            }

            _mapper.Map(dto, existingEntity);

            if (!isUpdate)
            {
                var validationResult = await _validator.ValidateAsync(existingEntity);
                if (!validationResult.IsValid)
                {
                    _logger.LogError("Validation failed during update of {EntityName}: {Errors}",
                        entityName, string.Join(", ", validationResult.Errors.Select(e => e.ErrorMessage)));
                    throw new ValidationException(validationResult.Errors);
                }
            }

            _dbSet.Update(existingEntity);
            await _context.SaveChangesAsync();
            return _mapper.Map<TDto>(existingEntity);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating {EntityName}: {Id}", entityName, id);
            throw;
        }
    }

    public virtual async Task<bool> SoftDeleteAsync(int id, string propertyName)
    {
        try
        {
            var entity = await _dbSet.FindAsync(id);
            if (entity == null)
            {
                _logger.LogWarning("Entity not found for soft delete: {EntityId}", id);
                return false;
            }

            _context.Entry(entity).Property(propertyName).CurrentValue = true;
            await _context.SaveChangesAsync();

            _logger.LogInformation("Entity soft deleted successfully: {EntityId}", id);
            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while soft-deleting entity: {EntityId}", id);
            throw;
        }
    }

    public virtual async Task<bool> HardDeleteAsync(int id, string propertyName)
    {
        try
        {
            var rowsDeleted = await _dbSet
                .Where(e => EF.Property<int>(e, propertyName) == id)
                .ExecuteDeleteAsync();

            if (rowsDeleted == 0)
            {
                _logger.LogWarning("{propertyName} not found for hard delete: {id}", propertyName, id);
                throw new KeyNotFoundException($"{propertyName} with ID {id} not found.");
            }

            _logger.LogInformation("Successfully hard deleted {propertyName} with ID {id}", propertyName, id);
            return rowsDeleted > 0;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error hard deleting {propertyName}: {id}", propertyName, id);
            throw;
        }
    }

    public virtual async Task<bool> ExistsAsync(int id)
    {
        try
        {
            return await _dbSet.FindAsync(id) != null;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error checking existence of entity with id: {Id}", id);
            throw;
        }
    }
}
