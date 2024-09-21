using Testcontainers.MsSql;

namespace StileStream.Wms.Inventory.Integration.Tests.Fixtures;
public class SqlServerTestFixture : IAsyncLifetime
{
    private MsSqlContainer? _sqlContainer;
    public string ConnectionString { get; private set; } = string.Empty;

    public virtual async Task InitializeAsync() => await StartSqlContainerAsync();

    private async Task StartSqlContainerAsync()
    {
        _sqlContainer = new MsSqlBuilder()
            .WithImage("mcr.microsoft.com/mssql/server:2019-latest")
            .WithPassword("yourStrong(!)Password")
            .Build();

        await _sqlContainer.StartAsync();
        ConnectionString = _sqlContainer.GetConnectionString();
    }

    public virtual async Task DisposeAsync()
    {
        if (_sqlContainer != null)
        {
            await _sqlContainer.DisposeAsync();
        }
    }
}
