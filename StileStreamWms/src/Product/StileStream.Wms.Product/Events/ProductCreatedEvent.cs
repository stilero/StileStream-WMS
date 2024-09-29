using StileStream.Wms.Product.Entities;
using StileStream.Wms.SharedKernel.Domain.Interfaces;

namespace StileStream.Wms.Product.Events;

public record ProductCreatedEvent(ProductEntity Product) : IDomainEvent;
