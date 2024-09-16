using SharedKernel.Domain.Interfaces;
namespace StileStream.Wms.Inventory.Infrastructure.Data.Repositories;
public class InventoryUnitOfWork : IUnitOfWork
{
    private readonly InventoryServiceDbContext _dbContext;

    public InventoryUnitOfWork(InventoryServiceDbContext dbContext)
    {
        _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
    }

    public async Task BeginTransactionAsync()
    {
        if (_dbContext == null)
        {
            throw new InvalidOperationException("DbContext is not initialized.");
        }
        try
        {
            await _dbContext.Database.BeginTransactionAsync();
        }
        catch (Exception ex)
        {
            // Log the exception details
            Console.WriteLine($"Error starting transaction: {ex.Message}");
            throw;
        }
    }
    public async Task CommitTransactionAsync() => await _dbContext.Database.CommitTransactionAsync();
    public async Task RollbackTransactionAsync() => await _dbContext.Database.RollbackTransactionAsync();
    public async Task SaveChangesAsync() => await _dbContext.SaveChangesAsync();
       
}
