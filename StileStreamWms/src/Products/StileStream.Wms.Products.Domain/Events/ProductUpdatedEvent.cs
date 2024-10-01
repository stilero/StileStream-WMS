using StileStream.Wms.Products.Domain.Entities;
using StileStream.Wms.SharedKernel.Domain.Interfaces;

namespace StileStream.Wms.Products.Domain.Events;
public record ProductUpdatedEvent(Product Product) : IDomainEvent;
