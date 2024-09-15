namespace SharedKernel.Domain.Interfaces;

public interface IUnitOfWork<TDbContext>
{
    Task BeginTransactionAsync();
    Task CommitTransactionAsync();
    Task RollbackTransactionAsync();
    Task SaveChangesAsync();
}
