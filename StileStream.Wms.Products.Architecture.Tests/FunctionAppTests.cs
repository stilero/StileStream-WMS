using NetArchTest.Rules;
using StileStream.Wms.Products.FunctionApp.Functions;

namespace StileStream.Wms.Products.Architecture.Tests;

public sealed class FunctionAppTests
{
    [Fact]
    public void FunctionAppLayer_Should_OnlyReference_ApplicationLayer()
    {
        var result = Types.InAssembly(typeof(CreateProductsFunction).Assembly)
            .Should()
            .NotHaveDependencyOnAny("Infrastructure", "Domain")
            .GetResult();

        Assert.True(result.IsSuccessful);
    }
}
