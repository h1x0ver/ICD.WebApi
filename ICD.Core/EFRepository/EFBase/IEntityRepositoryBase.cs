using System.Linq.Expressions;

namespace ICD.Core.EFRepository.EFBase;

public interface IEntityRepositoryBase<TEntity>
    where TEntity : class, /*IEntity, */new()
{
    Task<TEntity> GetAsync(Expression<Func<TEntity, bool>>? expression, int? skip = 0, params string[] includes);
    Task<List<TEntity>> GetAllAsync<TEntityOrderBy>(Expression<Func<TEntity, TEntityOrderBy>> orderBy, Expression<Func<TEntity, bool>>? expression, int? skip = 0, int? take = int.MaxValue, params string[] includes);
    Task CreateAsync(TEntity entity);
    Task UpdateAsync(TEntity entity);
    Task DeleteAsync(TEntity entity);
    Task SaveAsync();
}
