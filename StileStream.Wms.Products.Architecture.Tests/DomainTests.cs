using NetArchTest.Rules;
using StileStream.Wms.Products.Domain.ProductImport.Events;

namespace StileStream.Wms.Products.Architecture.Tests
{
    public sealed class DomainTests
    {
        [Fact]
        public void All_Classes_In_DomainLayer_AreSealed()
        {
            var result = Types.InAssembly(typeof(ProductImportCreated).Assembly)
                .That()
                .AreClasses()
                .Should()
                .BeSealed()
                .GetResult();

            Assert.True(result.IsSuccessful);
        }

        [Fact]
        public void DomainLayer_HasNo_ReferenceTo_OtherLayers()
        {
            var result = Types.InAssembly(typeof(ProductImportCreated).Assembly)
                .Should()
                .NotHaveDependencyOnAny("Application", "Infrastructure", "FunctionApp")
                .GetResult();

            Assert.True(result.IsSuccessful);
        }
    }
}