using System.Linq.Expressions;
using EShop.ProductService.Application.Interfaces.Repositories;
using EShop.ProductService.Domain.Common;
using EShop.ProductService.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace EShop.ProductService.Infrastructure.Repositories;

public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : BaseAuditableEntity
{
    protected DbSet<TEntity> _dbSet;

    public GenericRepository(ApplicationDbContext context)
    {
        _dbSet = context.Set<TEntity>();
    }

public async Task<TEntity> AddAsync(TEntity entity)
    {
        entity.CreatedAt = DateTime.UtcNow.AddHours(7);
        var result = await _dbSet.AddAsync(entity);
        return result.Entity;
    }

    public async Task AddRangeAsync(List<TEntity> entities)
    {
        foreach (var entity in entities)
        {
            entity.CreatedAt = DateTime.UtcNow.AddHours(7);
        }
        await _dbSet.AddRangeAsync(entities);
    }

    public async Task<int> CountAsync(Expression<Func<TEntity, bool>>? filter = null)
    {
        return filter == null
            ? await _dbSet.CountAsync()
            : await _dbSet.CountAsync(filter);
    }

    public async Task<TEntity?> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> expression, params Expression<Func<TEntity, object>>[] includes)
        => await includes
            .Aggregate(_dbSet!.AsQueryable(), (entity, property) => entity!.Include(property)).AsNoTracking()
            .Where(expression!)
            .FirstOrDefaultAsync();

    public async Task<List<TEntity>> GetAllAsync(params Expression<Func<TEntity, object>>[] includes) =>
    await includes.Aggregate(_dbSet.AsQueryable(), (entity, property) => entity.Include(property).IgnoreAutoIncludes())
    .OrderByDescending(x => x.CreatedAt)
    .ToListAsync();


    public async Task<TEntity?> GetByIdAsync(Guid id, params Expression<Func<TEntity, object>>[] includes)
    {
        return await includes
            .Aggregate(_dbSet.AsQueryable(), (entity, property) => entity.Include(property))
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id.Equals(id));
    }

    public void Remove(TEntity entity)
    {
        _dbSet.Remove(entity);
    }
    public void Update(TEntity entity)
    {
        entity.UpdatedAt = DateTime.UtcNow.AddHours(7);
        _dbSet.Update(entity);
    }

    public void UpdateRange(List<TEntity> entities)
    {
        foreach (var entity in entities)
        {
            entity.UpdatedAt = DateTime.UtcNow.AddHours(7);
        }
        _dbSet.UpdateRange(entities);
    }

    public async Task<List<TEntity>> WhereAsync(Expression<Func<TEntity, bool>> expression, params Expression<Func<TEntity, object>>[] includes)
    => await includes
        .Aggregate(_dbSet!.AsQueryable(), (entity, property) => entity.Include(property)).AsNoTracking()
        .Where(expression!)
        .OrderByDescending(x => x.CreatedAt)
        .ToListAsync();

    public async Task<bool> AnyAsync(Expression<Func<TEntity, bool>> expression)
    {
        return await _dbSet.AsNoTracking().AnyAsync(expression);
    }

    public async Task<IQueryable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> predicate, Func<IQueryable<TEntity>, IQueryable<TEntity>>? include = null)
    {
        IQueryable<TEntity> query = _dbSet;

        if (include != null)
        {
            query = include(query);
        }

        if (predicate != null)
        {
            query = query.Where(predicate);
        }

        return await Task.FromResult(query);
    }
}