using Shared.Domain.Interfaces;

namespace InventoryService.Domain.Events;

public record ProductUpdatedDomainEvent(Product Product) : IDomainEvent;
