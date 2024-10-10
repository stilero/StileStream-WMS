//using MediatR;
//using StileStream.Wms.Inventory.Domain.Products.Events;

//namespace StileStream.Wms.Inventory.Application.Products.ProductCreated.EventHandlers;

//public sealed class ProductCreatedHandler : INotificationHandler<ProductCreatedEvent>
//{
//    public Task Handle(ProductCreatedEvent notification, CancellationToken cancellationToken)
//    {
//        Console.WriteLine($"FROM EVENTHANDLER: Product created: {notification?.Product.Sku}");
//        return Task.CompletedTask;
//    }
//}
