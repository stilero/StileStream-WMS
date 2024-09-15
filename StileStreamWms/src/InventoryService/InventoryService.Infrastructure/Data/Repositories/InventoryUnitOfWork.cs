using SharedKernel.Infrastructure.Data;
namespace InventoryService.Infrastructure.Data.Repositories;
public class InventoryUnitOfWork : UnitOfWork<InventoryServiceDbContext>
{
    public InventoryUnitOfWork(InventoryServiceDbContext dbContext) : base(dbContext)
    {
    }
}
