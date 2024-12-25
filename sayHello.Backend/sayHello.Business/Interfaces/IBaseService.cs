using System.Linq.Expressions;

namespace sayHello.Business;

public interface IBaseService<TEntity, TDto> where TEntity : class where TDto : class
{
    Task<TDto?> FindBy(Expression<Func<TEntity, bool>> predicate);
    Task<IEnumerable<TDto>> GetAllAsync();
    Task<TDto> AddAsync<TCreateDto>(TCreateDto createDto, string entityName);
    Task<TDto?> UpdateAsync(int id, TDto dto, string entityName );
    Task<bool> SoftDeleteAsync(int id , string propertyName);
    Task<bool> HardDeleteAsync(int id, string propertyName);
    Task<bool> ExistsAsync(int id);
}