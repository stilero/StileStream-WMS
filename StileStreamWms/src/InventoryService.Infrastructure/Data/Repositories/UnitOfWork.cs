using Microsoft.EntityFrameworkCore;
using Shared.Domain.Interfaces;

namespace InventoryService.Infrastructure.Data.Repositories;

public class UnitOfWork<TDbContext>(TDbContext context) : IUnitOfWork, IDisposable
    where TDbContext : DbContext
{
    private readonly TDbContext _context = context;

    public async Task<int> Commit(CancellationToken cancellationToken = default) 
        => await _context.SaveChangesAsync(cancellationToken);

    public void Dispose()
    {
        _context.Dispose();
        GC.SuppressFinalize(this);
    }
}
