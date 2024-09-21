using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;


namespace StileStream.Wms.Inventory.Infrastructure.Data;

public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<InventoryDbContext>
{
    public InventoryDbContext CreateDbContext(string[] args)
    {
        if (args.Length != 1)
            throw new ApplicationException("You need to provide an argument that contains the database connection string. Command could be \"dotnet ef database update -- \"<connection string>\" ");

        var connectionString = args[0];

        if (string.IsNullOrWhiteSpace(connectionString))
            throw new ApplicationException("Connection string cant be empty...");

        var optionsBuilder = new DbContextOptionsBuilder<InventoryDbContext>();
        optionsBuilder.UseSqlServer(connectionString);
        return new InventoryDbContext(optionsBuilder.Options);
    }
}
