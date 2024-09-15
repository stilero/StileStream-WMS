using InventoryService.Domain.Entities;
using Shared.Domain.Interfaces;

namespace InventoryService.Domain.Events;

public record ProductUpdatedEvent(Product Product) : IDomainEvent;
