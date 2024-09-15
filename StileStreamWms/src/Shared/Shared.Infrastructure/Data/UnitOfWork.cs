using Microsoft.EntityFrameworkCore;

using SharedKernel.Domain.Interfaces;

namespace SharedKernel.Infrastructure.Data;
public class UnitOfWork<TDbContext>(TDbContext dbContext)
    : IUnitOfWork<TDbContext>
    where TDbContext : DbContext
{
    private readonly TDbContext _dbContext = dbContext;

    public async Task BeginTransactionAsync() => await _dbContext.Database.BeginTransactionAsync();
    public async Task CommitTransactionAsync() => await _dbContext.Database.CommitTransactionAsync();
    public async Task RollbackTransactionAsync() => await _dbContext.Database.RollbackTransactionAsync();
    public async Task SaveChangesAsync() => await _dbContext.SaveChangesAsync();
}
