using System.Text;

using FluentAssertions;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

using Newtonsoft.Json;

using StileStream.Wms.Products.Application.Features.Products.CreateProducts.Contracts;
using StileStream.Wms.Products.Application.Features.Products.ImportProducts.Contracts;
using StileStream.Wms.Products.Domain.ProductImport.Events;
using StileStream.Wms.Products.Domain.ProductImport.ValueObjects;
using StileStream.Wms.Products.Domain.Products.Events;
using StileStream.Wms.Products.Infrastructure;
using StileStream.Wms.Products.Integration.Tests.Fakers;
using StileStream.Wms.Products.Integration.Tests.Fixtures;

namespace StileStream.Wms.Products.Integration.Tests;

public class ProductImportTests : IClassFixture<AzureFunctionFixture>
{
    private readonly AzureFunctionFixture _fixture;
    private readonly HttpClient? _httpClient;

    public ProductImportTests(AzureFunctionFixture fixture)
    {
        _fixture = fixture;
        _httpClient = _fixture.HttpClient;
    }

    [Fact]
    public async Task Request_GivenValidData_PersistsProductImportAndOutboxMessages_Successfully()
    {
        //Arrange
        var dbContext = _fixture.Host!.Services.GetRequiredService<ProductsDbContext>();
        var productCount = 5;
        var requestContent = RequestFaker.ProductImportRequestFaker(ImportType.Add, 5).Generate();

        using var request = new HttpRequestMessage(HttpMethod.Post, "api/products/import")
        {
            Content = new StringContent(JsonConvert.SerializeObject(requestContent), Encoding.UTF8, "application/json")
        };

        //Act
        var response = await _httpClient!.SendAsync(request);
        dbContext.ChangeTracker.Clear();

        //Assert
        response.EnsureSuccessStatusCode();
        var productImports = await dbContext.ProductImports.ToListAsync();
        productImports.Should().NotBeNullOrEmpty();
        productImports.Should().HaveCount(1);      
        var outboxMessages = await dbContext.OutboxMessages.ToListAsync();
        outboxMessages.Should().NotBeNullOrEmpty();
        outboxMessages.Where(o => o.Type == nameof(ProductImportCreatedEvent)).Should().HaveCount(1);
        outboxMessages.Where(o => o.Type == nameof(ProductImportStagedEvent)).Should().HaveCount(1);
    }
}
