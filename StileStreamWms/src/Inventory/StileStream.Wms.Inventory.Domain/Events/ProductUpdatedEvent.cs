using StileStream.Wms.SharedKernel.Domain.Interfaces;

using StileStream.Wms.Inventory.Domain.Entities;

namespace StileStream.Wms.Inventory.Domain.Events;

public record ProductUpdatedEvent(Product Product) : IDomainEvent;
