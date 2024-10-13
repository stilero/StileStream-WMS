using Bogus;

using StileStream.Wms.Products.Application.Features.Products.CreateProducts.Contracts;

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
}
