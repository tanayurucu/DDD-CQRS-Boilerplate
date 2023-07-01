using CleanArchitecture.Application.Common.Interfaces.Persistence.Repositories;
using CleanArchitecture.Domain.Common.Abstractions;

using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Persistence.Common.Abstractions;

internal abstract class AsyncRepository<TEntity, TId> : IAsyncRepository<TEntity, TId>
    where TEntity : AggregateRoot<TId>
    where TId : notnull
{
    protected readonly CleanArchitectureDbContext _dbContext;

    protected AsyncRepository(CleanArchitectureDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task AddAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        await _dbContext.Set<TEntity>().AddAsync(entity, cancellationToken);
    }

    public async Task AddRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default)
    {
        await _dbContext.Set<TEntity>().AddRangeAsync(entities, cancellationToken);
    }

    public void Update(TEntity entity)
    {
        _dbContext.Set<TEntity>().Update(entity);
    }

    public void Remove(TEntity entity)
    {
        _dbContext.Set<TEntity>().Remove(entity);
    }

    public void RemoveRange(IEnumerable<TEntity> entities)
    {
        _dbContext.Set<TEntity>().RemoveRange(entities);
    }

    public Task<TEntity?> FindByIdAsync(TId id, CancellationToken cancellationToken = default)
    {
        return _dbContext.Set<TEntity>().FirstOrDefaultAsync(entity => entity.Id.Equals(id), cancellationToken);
    }

    public Task<List<TEntity>> FindAllAsync(CancellationToken cancellationToken = default)
    {
        return _dbContext.Set<TEntity>().ToListAsync(cancellationToken);
    }
}