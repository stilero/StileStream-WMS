using InventoryService.Domain.Entities;

using SharedKernel.Domain.Interfaces;

namespace InventoryService.Domain.Events;

public record ProductCreatedEvent(Product Product) : IDomainEvent;
