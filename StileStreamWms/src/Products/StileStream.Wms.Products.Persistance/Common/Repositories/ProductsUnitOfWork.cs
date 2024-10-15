using StileStream.Wms.Products.Persistance;
using StileStream.Wms.SharedKernel.Domain.Interfaces;
namespace StileStream.Wms.Products.Persistance.Common.Repositories;
public class ProductsUnitOfWork : IUnitOfWork
{
    private readonly ProductsDbContext _dbContext;
    private readonly List<IAggregateRoot> _trackedEntities = [];

    public ProductsUnitOfWork(ProductsDbContext dbContext)
    {
        _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
    }

    public IReadOnlyList<IAggregateRoot> GetTrackedEntities() => _trackedEntities;

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

    public void TrackEntity(IAggregateRoot entity)
    {
        if (!_trackedEntities.Contains(entity))
        {
            _trackedEntities.Add(entity);
        }
    }

    public void TranckEntities(IEnumerable<IAggregateRoot> entities)
    {
        ArgumentNullException.ThrowIfNull(entities, nameof(entities));
        foreach (var entity in entities)
        {
            TrackEntity(entity);
        }
    }
}
