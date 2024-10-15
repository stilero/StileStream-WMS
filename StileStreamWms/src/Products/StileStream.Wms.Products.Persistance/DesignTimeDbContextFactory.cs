using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace StileStream.Wms.Products.Persistance;

public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<ProductsDbContext>
{
    public ProductsDbContext CreateDbContext(string[] args)
    {
        ArgumentNullException.ThrowIfNull(args, nameof(args));

        if (args.Length != 1)
        {
            throw new ArgumentException("You need to provide an argument that contains the database connection string. Command could be \"dotnet ef database update -- \"<connection string>\" ");
        }

        var connectionString = args[0];

        if (string.IsNullOrWhiteSpace(connectionString))
        {
            throw new ArgumentException("Connection string cant be empty...");
        }

        var optionsBuilder = new DbContextOptionsBuilder<ProductsDbContext>();
        optionsBuilder.UseSqlServer(connectionString);
        return new ProductsDbContext(optionsBuilder.Options);
    }
}
