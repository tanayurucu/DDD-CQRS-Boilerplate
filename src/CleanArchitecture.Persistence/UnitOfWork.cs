using CleanArchitecture.Application.Common.Interfaces.Persistence;

using Microsoft.EntityFrameworkCore.Storage;

namespace CleanArchitecture.Persistence;

internal sealed class UnitOfWork : IUnitOfWork
{
    private readonly CleanArchitectureDbContext _dbContext;

    public UnitOfWork(CleanArchitectureDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return _dbContext.SaveChangesAsync(cancellationToken);
    }

    public Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken cancellationToken = default)
    {
        return _dbContext.Database.BeginTransactionAsync(cancellationToken);
    }
}