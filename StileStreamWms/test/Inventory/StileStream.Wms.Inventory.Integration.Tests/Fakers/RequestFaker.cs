//using Bogus;

//using StileStream.Wms.Inventory.Application.Products.ProductImport.Requests;

//namespace StileStream.Wms.Inventory.Integration.Tests.Fakers;
//public static class RequestFaker
//{
//    public static Faker<ImportProductRequest> ImportProductRequest() => new Faker<ImportProductRequest>()
//        .CustomInstantiator(f => new ImportProductRequest(
//            f.Name.FirstName(), 
//            f.Commerce.Ean13(), 
//            f.Commerce.ProductDescription(), 
//            f.Company.CompanyName(), 
//            f.Commerce.Categories(1)[0], 
//            f.PickRandomParam("Active", "Inactive"), 
//            f.Internet.UserName(), 
//            f.Internet.UserName()));
//}
