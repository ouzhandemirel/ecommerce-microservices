using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore.Query;
using Shared.Domain.Abstractions;

namespace Shared.Application.Persistence;

public interface IRepository<TEntity, TEntityId>
    where TEntity : Entity<TEntityId>
{
    Task<TEntity?> GetAsync(
        Expression<Func<TEntity, bool>> predicate,
        Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default);

    Task<ICollection<TEntity>> GetListAsync(
        Expression<Func<TEntity, bool>>? predicate = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
        Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    );

    Task<bool> AnyAsync(
        Expression<Func<TEntity, bool>>? predicate = null,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    );

    Task<TEntity> AddAsync(TEntity entity);

    Task<ICollection<TEntity>> AddRangeAsync(ICollection<TEntity> entities);

    Task<TEntity> UpdateAsync(TEntity entity);

    Task<ICollection<TEntity>> UpdateRangeAsync(ICollection<TEntity> entities);

    Task<TEntity> DeleteAsync(TEntity entity);

    Task<ICollection<TEntity>> DeleteRangeAsync(ICollection<TEntity> entities);

}