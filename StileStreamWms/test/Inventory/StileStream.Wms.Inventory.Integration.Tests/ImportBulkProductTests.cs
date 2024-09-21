using System.Text;

using FluentAssertions;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

using Newtonsoft.Json;

using StileStream.Wms.Inventory.Application.Products.ProductImport.Requests;
using StileStream.Wms.Inventory.Infrastructure.Data;
using StileStream.Wms.Inventory.Integration.Tests.Fakers;
using StileStream.Wms.Inventory.Integration.Tests.Fixtures;

namespace StileStream.Wms.Inventory.Integration.Tests;

public class ImportBulkProductTests : IClassFixture<AzureFunctionFixture>
{
    private readonly AzureFunctionFixture _fixture;
    private readonly HttpClient? _httpClient;

    public ImportBulkProductTests(AzureFunctionFixture fixture)
    {
        _fixture = fixture;
        _httpClient = _fixture.HttpClient;
    }

    [Fact]
    public async Task Request_GivenValidData_StoresProductToDatabaseSuccessfully()
    {
        //Arrange
        var dbContext = _fixture.Host!.Services.GetRequiredService<InventoryDbContext>();
        var productCount = 5;
        var requestContent = new ImportBulkProductRequest(Products: RequestFaker.ImportProductRequest().Generate(productCount));

        using var request = new HttpRequestMessage(HttpMethod.Post, "api/products/import")
        {
            Content = new StringContent(JsonConvert.SerializeObject(requestContent), Encoding.UTF8, "application/json")
        };

        //Act
        var response = await _httpClient!.SendAsync(request);

        //Assert
        response.EnsureSuccessStatusCode();
        var products = await dbContext.Products.ToListAsync();
        products.Should().NotBeNullOrEmpty();
        products.Should().HaveCount(productCount);
    }
}
