var builder = DistributedApplication.CreateBuilder(args);

builder.AddProject<Projects.InventoryService_FunctionApp>("inventoryservice-functionapp");

builder.Build().Run();
