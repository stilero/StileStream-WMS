using System.Text;

using FluentAssertions;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

using Newtonsoft.Json;

using StileStream.Wms.Products.Application.Features.CreateProducts.Contracts;
using StileStream.Wms.Products.Domain.Products.Events;
using StileStream.Wms.Products.Infrastructure;
using StileStream.Wms.Products.Integration.Tests.Fakers;
using StileStream.Wms.Products.Integration.Tests.Fixtures;

namespace StileStream.Wms.Products.Integration.Tests;

public class CreateProductsTests : IClassFixture<AzureFunctionFixture>
{
    private readonly AzureFunctionFixture _fixture;
    private readonly HttpClient? _httpClient;

    public CreateProductsTests(AzureFunctionFixture fixture)
    {
        _fixture = fixture;
        _httpClient = _fixture.HttpClient;
    }

    [Fact]
    public async Task Request_GivenValidData_StoresProductToDatabaseSuccessfully()
    {
        //Arrange
        var dbContext = _fixture.Host!.Services.GetRequiredService<ProductsDbContext>();
        var productCount = 5;
        var requestContent = new CreateProductsRequest(Products: RequestFaker.CreateProductRequestFaker().Generate(productCount));

        using var request = new HttpRequestMessage(HttpMethod.Post, "api/products/import")
        {
            Content = new StringContent(JsonConvert.SerializeObject(requestContent), Encoding.UTF8, "application/json")
        };

        //Act
        var response = await _httpClient!.SendAsync(request);
        dbContext.ChangeTracker.Clear();

        //Assert
        response.EnsureSuccessStatusCode();
        var products = await dbContext.Products.ToListAsync();
        products.Should().NotBeNullOrEmpty();
        products.Should().HaveCount(productCount);
        products.Where(p => requestContent.Products.Select(Products => Products.Name).Contains(p.Name)).Should().HaveCount(productCount);
        var outboxMessages = await dbContext.OutboxMessages.ToListAsync();
        outboxMessages.Should().NotBeNullOrEmpty();
        outboxMessages.Where(o => o.Type == nameof(ProductCreatedEvent)).Should().HaveCount(productCount);
    }
}
