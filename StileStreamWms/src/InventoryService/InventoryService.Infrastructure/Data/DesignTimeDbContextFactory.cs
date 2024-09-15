using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;


namespace InventoryService.Infrastructure.Data;

public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<InventoryServiceDbContext>
{
    public InventoryServiceDbContext CreateDbContext(string[] args)
    {
        if (args.Length != 1) throw new ApplicationException("You need to provide an argument that contains the database connection string. Command could be \"dotnet ef database update -- \"<connection string>\" ");

        var connectionString = args[0];

        if (string.IsNullOrWhiteSpace(connectionString))
            throw new ApplicationException("Connection string cant be empty...");

        var optionsBuilder = new DbContextOptionsBuilder<InventoryServiceDbContext>();
        optionsBuilder.UseSqlServer(connectionString);
        return new InventoryServiceDbContext(optionsBuilder.Options);
    }
}
