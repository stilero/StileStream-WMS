using MassTransit;

namespace StileStream.Wms.Products.Application.Features.ProductImports.ProductImportProcess.Commands;
public sealed class TestConsumer : IConsumer<TestEvent>
{
    public Task Consume(ConsumeContext<TestEvent> context)
    {
        Console.WriteLine($"Received: {context.Message.Id} - {context.Message.Name}");
        return Task.CompletedTask;
    }
}

