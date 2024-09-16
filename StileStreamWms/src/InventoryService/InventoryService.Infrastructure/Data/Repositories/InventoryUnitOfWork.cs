using SharedKernel.Domain.Interfaces;
namespace InventoryService.Infrastructure.Data.Repositories;
public class InventoryUnitOfWork : IUnitOfWork
{
    private readonly InventoryServiceDbContext _dbContext;

    public InventoryUnitOfWork(InventoryServiceDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task BeginTransactionAsync() => await _dbContext.Database.BeginTransactionAsync();
    public async Task CommitTransactionAsync() => await _dbContext.Database.CommitTransactionAsync();
    public async Task RollbackTransactionAsync() => await _dbContext.Database.RollbackTransactionAsync();
    public async Task SaveChangesAsync() => await _dbContext.SaveChangesAsync();
}
