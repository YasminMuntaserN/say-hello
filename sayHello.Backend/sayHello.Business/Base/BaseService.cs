using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using sayHello.DataAccess;
namespace sayHello.Business.Base;

public abstract class BaseService<TEntity, TDto> : IBaseService<TEntity, TDto> where TEntity : class where TDto : class
{
    protected readonly AppDbContext _context;
    protected readonly DbSet<TEntity> _dbSet;
    protected readonly ILogger<BaseService<TEntity,TDto>> _logger;
    protected readonly IMapper _mapper; 

    protected BaseService(
        AppDbContext context,
        ILogger<BaseService<TEntity,TDto>> logger,
        IMapper mapper)
    {
        _context = context;
        _dbSet = context.Set<TEntity>();
        _logger = logger;
        _mapper = mapper;
    }

    public virtual async Task<TDto?> GetByIdAsync(int id)
    {
        try
        {
            var entity = await _dbSet.FindAsync(id);
            return entity != null ? _mapper.Map<TDto>(entity) : null;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving entity by id: {Id}", id);
            throw;
        }
    }

    public virtual async Task<IEnumerable<TDto>> GetAllAsync()
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

    public virtual async Task<TDto> CreateAsync(TDto dto)
    {
        try
        {
            var entity = _mapper.Map<TEntity>(dto);
            await _dbSet.AddAsync(entity);
            await _context.SaveChangesAsync();
            return _mapper.Map<TDto>(entity);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating entity");
            throw;
        }
    }

    public virtual async Task UpdateAsync(TDto dto)
    {
        try
        {
            var entity = _mapper.Map<TEntity>(dto);
            _dbSet.Update(entity);
            await _context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating entity");
            throw;
        }
    }

    public virtual async Task SoftDeleteAsync(int id ,string propertyName )
    {
        try
        {
            await _dbSet
            .ExecuteUpdateAsync(set => set
                .SetProperty(e => EF.Property<bool>(e, propertyName), true));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while soft-deleting entity");
            throw;
        }
    }

    public virtual async Task HardDeleteAsync(int id)
    {
        try
        {
            var dto = await GetByIdAsync(id);
            if (dto != null)
            {
                var entity = _mapper.Map<TEntity>(dto);
                _dbSet.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error deleting entity with id: {Id}", id);
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
