using Bogus;

using StileStream.Wms.Products.Application.Features.Products.CreateProducts.Contracts;
using StileStream.Wms.Products.Application.Features.Products.ImportProducts.Contracts;
using StileStream.Wms.Products.Domain.ProductImport.ValueObjects;

namespace StileStream.Wms.Products.Integration.Tests.Fakers;
public static class RequestFaker
{
    public static Faker<CreateProductRequest> CreateProductRequestFaker() => new Faker<CreateProductRequest>()
        .CustomInstantiator(f => new CreateProductRequest(
            f.Name.FirstName(),
            f.Commerce.Ean13(),
            f.Commerce.ProductDescription(),
            f.Company.CompanyName(),
            f.Commerce.Categories(1)[0],
            f.PickRandomParam("Active", "Inactive"),
            f.Internet.UserName(),
            f.Internet.UserName()));

    public static Faker<ProductImportRequest> ProductImportRequestFaker(ImportType importType, int productCount) => new Faker<ProductImportRequest>()
        .CustomInstantiator(f => new ProductImportRequest(
            importType,
            new Faker<ProductData>().CustomInstantiator(f => new ProductData(
                f.Name.FirstName(),
                f.Commerce.Ean13(),
                f.Commerce.ProductDescription(),
                f.Company.CompanyName(),
                f.Commerce.Categories(1)[0],
                f.PickRandomParam("Active", "Inactive")
                )).Generate(productCount)));
            
}
