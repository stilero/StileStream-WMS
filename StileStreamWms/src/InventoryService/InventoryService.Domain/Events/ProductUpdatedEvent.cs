using InventoryService.Domain.Entities;

using SharedKernel.Domain.Interfaces;

namespace InventoryService.Domain.Events;

public record ProductUpdatedEvent(Product Product) : IDomainEvent;
