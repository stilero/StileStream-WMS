using System.Linq.Expressions;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

using StileStream.Wms.Inventory.Infrastructure.Data.OutboxMessages.Entities;
using StileStream.Wms.Inventory.Infrastructure.Data.Products;
using StileStream.Wms.SharedKernel.Infrastructure.Data.Interfaces;

namespace StileStream.Wms.Inventory.Infrastructure.Data;

public class InventoryDbContext : DbContext
{
    public InventoryDbContext(DbContextOptions<InventoryDbContext> options) : base(options)
    {
    }
    public DbSet<ProductEntity> Products { get; set; }
    public DbSet<OutboxMessage> OutboxMessages { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        ArgumentNullException.ThrowIfNull(modelBuilder, nameof(modelBuilder));
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(InventoryDbContext).Assembly);

        foreach (var entityType in modelBuilder.Model.GetEntityTypes())
        {
            if (entityType.ClrType is null)
            {
                continue;
            }

            if (entityType.ClrType.GetInterfaces().Contains(typeof(IAuditable)))
            {
                ConfigureAuditableEntity(modelBuilder, entityType);
            }
            if (entityType.ClrType.GetInterfaces().Contains(typeof(ISoftDeleteable)))
            {
                ConfigureSoftDeleteableEntity(modelBuilder, entityType);
            }            
        }
    }

    private static void ConfigureAuditableEntity(ModelBuilder modelBuilder, IMutableEntityType entityType)
    {
        modelBuilder.Entity(entityType.ClrType).Property<DateTime>("CreatedAt");
        modelBuilder.Entity(entityType.ClrType).Property<string>("CreatedBy");
        modelBuilder.Entity(entityType.ClrType).Property<DateTime>("UpdatedAt");
        modelBuilder.Entity(entityType.ClrType).Property<string>("UpdatedBy");
    }

    private static void ConfigureSoftDeleteableEntity(ModelBuilder modelBuilder, IMutableEntityType entityType)
    {
        modelBuilder.Entity(entityType.ClrType).Property<bool>("IsDeleted");
        modelBuilder.Entity(entityType.ClrType).HasQueryFilter(GetIsDeletedRestriction(entityType.ClrType));
    }

    private static LambdaExpression GetIsDeletedRestriction(Type type)
    {
        var parameter = Expression.Parameter(type, "e");
       var isDeletedProperty = Expression.Property(parameter, "IsDeleted");
        var compareExpression = Expression.Equal(isDeletedProperty, Expression.Constant(false));
        return Expression.Lambda(compareExpression, parameter);
    }

    public override int SaveChanges()
    {
        UpdateShadowProperties();
        return base.SaveChanges();
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        UpdateShadowProperties();
        return base.SaveChangesAsync(cancellationToken);
    }

    private void UpdateShadowProperties()
    {

        foreach (var entry in ChangeTracker.Entries())
        {
            var entity = entry.Entity;
            var entityType = entity.GetType();

            if (entry.State == EntityState.Added)
            {
                if(entity is IAuditable)
                {
                    entry.CurrentValues["CreatedAt"] = DateTime.UtcNow;
                    entry.CurrentValues["CreatedBy"] = "system";
                }

                if(entity is ISoftDeleteable)
                {
                    entry.CurrentValues["IsDeleted"] = false;
                }
            }
            else if (entry.State == EntityState.Modified)
            {
                if (entity is IAuditable)
                {
                    entry.CurrentValues["UpdatedAt"] = DateTime.UtcNow;
                    entry.CurrentValues["UpdatedBy"] = "system";
                }
            }
            else if (entry.State == EntityState.Deleted)
            {
                if (entity is ISoftDeleteable)
                {
                    entry.State = EntityState.Modified;
                    entry.CurrentValues["IsDeleted"] = true;
                }

                if (entity is IAuditable)
                {
                    entry.CurrentValues["UpdatedAt"] = DateTime.UtcNow;
                    entry.CurrentValues["UpdatedBy"] = "system";
                }
            }
        }
    }
}
