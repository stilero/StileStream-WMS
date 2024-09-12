using System.Data;

namespace Shared.Domain.Interfaces;

public interface IUnitOfWork
{
    Task<int> Commit(CancellationToken cancellationToken = default);
}
