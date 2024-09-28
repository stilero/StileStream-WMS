namespace StileStream.Wms.SharedKernel.Infrastructure.Data.Interfaces;
public interface ISoftDeleteable
{
    bool IsDeleted { get; set; }
}
