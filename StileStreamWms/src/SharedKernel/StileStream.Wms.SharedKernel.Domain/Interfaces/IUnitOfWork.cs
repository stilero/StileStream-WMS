namespace StileStream.Wms.SharedKernel.Domain.Interfaces;

public interface IUnitOfWork
{
    Task BeginTransactionAsync();
    Task CommitTransactionAsync();
    Task RollbackTransactionAsync();
    Task SaveChangesAsync();
    //void TrackEntity(IAggregateRoot entity);
    //void TranckEntities(IEnumerable<IAggregateRoot> entities);

    //IReadOnlyList<IAggregateRoot> GetTrackedEntities();
}
