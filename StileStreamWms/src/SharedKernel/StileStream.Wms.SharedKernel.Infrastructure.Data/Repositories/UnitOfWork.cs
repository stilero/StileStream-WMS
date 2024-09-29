using Microsoft.EntityFrameworkCore;

using StileStream.Wms.SharedKernel.Domain.Interfaces;

namespace StileStream.Wms.SharedKernel.Infrastructure.Data.Repositories;
public class UnitOfWork(DbContext dbContext) : IUnitOfWork
{
    private readonly DbContext _dbContext = dbContext;

    public async Task BeginTransactionAsync() => await _dbContext.Database.BeginTransactionAsync();
    public async Task CommitTransactionAsync() => await _dbContext.Database.CommitTransactionAsync();
    public async Task RollbackTransactionAsync() => await _dbContext.Database.RollbackTransactionAsync();
    public async Task SaveChangesAsync() => await _dbContext.SaveChangesAsync();
}
