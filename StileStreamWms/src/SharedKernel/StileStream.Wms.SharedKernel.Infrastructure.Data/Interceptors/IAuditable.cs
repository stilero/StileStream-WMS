namespace StileStream.Wms.SharedKernel.Infrastructure.Data.Interceptors;

public interface IAuditable
{
    DateTime CreatedOn { get; set; }
    string CreatedBy { get; set; }
    DateTime UpdatedOn { get; set; }
    string UpdatedBy { get; set; }
}
