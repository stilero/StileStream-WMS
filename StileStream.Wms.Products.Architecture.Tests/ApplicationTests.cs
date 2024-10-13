using NetArchTest.Rules;
using StileStream.Wms.Products.Application.Features.Products.CreateProducts;
using StileStream.Wms.Products.Domain.Products.Events;

namespace StileStream.Wms.Products.Architecture.Tests
{
    public sealed class ApplicationTests
    {
        [Fact]
        public void ApplicationLayer_Should_OnlyReference_DomainLayer()
        {
            var applicationNamespace = typeof(CreateProductsCommand).Namespace;
            var domainNamespace = typeof(ProductCreatedEvent).Namespace;

            var result = Types
                .InAssembly(typeof(CreateProductsCommand).Assembly)
                .Should()
                .NotHaveDependencyOnAny("Infrastructure", "FunctionApp")
                .GetResult();

            Assert.True(result.IsSuccessful);
        }
    }
}
