using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Shared.Application.Persistence;
using Shared.Domain.Abstractions;

namespace Shared.Infrastructure.Persistence;

public class EfCoreRepository<TEntity, TEntityId, TContext> : IRepository<TEntity, TEntityId>
    where TEntity : Entity<TEntityId>
    where TContext : DbContext
{
    private readonly TContext _context;

    public EfCoreRepository(TContext context)
    {
        _context = context;
    }

    private IQueryable<TEntity> Query() => _context.Set<TEntity>();

    public async Task<TEntity> AddAsync(TEntity entity)
    {
        await _context.AddAsync(entity);
        return entity;
    }

    public async Task<ICollection<TEntity>> AddRangeAsync(ICollection<TEntity> entities)
    {
        await _context.AddRangeAsync(entities);
        return entities;
    }

    public async Task<bool> AnyAsync(
        Expression<Func<TEntity, bool>>? predicate = null, 
        bool withDeleted = false, 
        bool enableTracking = true, 
        CancellationToken cancellationToken = default)
    {
        IQueryable<TEntity> queryable = Query();
        if (!enableTracking)
            queryable = queryable.AsNoTracking();
        if(withDeleted)
            queryable = queryable.IgnoreQueryFilters();
        if(predicate != null)
            queryable = queryable.Where(predicate);
        return await queryable.AnyAsync(cancellationToken);
    }

    public async Task<TEntity> DeleteAsync(TEntity entity)
    {
        _context.Remove(entity);
        return await Task.FromResult(entity);
    }

    public async Task<ICollection<TEntity>> DeleteRangeAsync(ICollection<TEntity> entities)
    {
        _context.RemoveRange(entities);
        return await Task.FromResult(entities);
    }

    public async Task<TEntity?> GetAsync(
        Expression<Func<TEntity, bool>> predicate, 
        Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null,
        bool withDeleted = false, bool enableTracking = true, 
        CancellationToken cancellationToken = default)
    {
        IQueryable<TEntity> queryable = Query();
        if (!enableTracking)
            queryable = queryable.AsNoTracking();
        if (include != null)
            queryable = include(queryable);
        if (withDeleted)
            queryable = queryable.IgnoreQueryFilters();

        return await queryable.FirstOrDefaultAsync(predicate, cancellationToken);
    }

    public async Task<ICollection<TEntity>> GetListAsync(
        Expression<Func<TEntity, bool>>? predicate = null, 
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null, 
        Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null,
        bool withDeleted = false, 
        bool enableTracking = true, 
        CancellationToken cancellationToken = default)
    {
        IQueryable<TEntity> queryable = Query();

        if (!enableTracking)
            queryable = queryable.AsNoTracking();
        if (include != null)
            queryable = include(queryable);
        if (withDeleted)
            queryable = queryable.IgnoreQueryFilters();
        if (predicate != null)
            queryable = queryable.Where(predicate);
        if (orderBy != null)
            return await orderBy(queryable).ToListAsync(cancellationToken);

        return await queryable.ToListAsync(cancellationToken);
    }

    public async Task<TEntity> UpdateAsync(TEntity entity)
    {
        _context.Update(entity);
        return await Task.FromResult(entity);
    }

    public async Task<ICollection<TEntity>> UpdateRangeAsync(ICollection<TEntity> entities)
    {
        _context.UpdateRange(entities);
        return await Task.FromResult(entities);
    }
}