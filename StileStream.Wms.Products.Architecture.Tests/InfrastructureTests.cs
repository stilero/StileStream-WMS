using NetArchTest.Rules;
using StileStream.Wms.Products.Infrastructure;

namespace StileStream.Wms.Products.Architecture.Tests;

public sealed class InfrastructureTests
{
    [Fact]
    public void InfrastructureLayer_Should_OnlyReference_ApplicationLayer()
    {
        var result = Types.InAssembly(typeof(ProductsDbContext).Assembly)
            .Should()
            .NotHaveDependencyOnAny("Domain", "FunctionApp")
            .GetResult();

        Assert.True(result.IsSuccessful);
    }
}
