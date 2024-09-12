using Shared.Domain.Interfaces;

namespace InventoryService.Domain.Events;

public record ProductCreatedDomainEvent(Product Product) : IDomainEvent;
