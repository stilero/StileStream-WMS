﻿using StileStream.Wms.Products.Application.Features.Products.ImportProducts.Contracts;
using StileStream.Wms.SharedKernel.Application.Models.Results;

namespace StileStream.Wms.Products.Application.Features.Products.ImportProducts.Interfaces;

public interface IProductImportService
{
    Task<Result<Guid>> CreateNew(ProductImportRequest request, CancellationToken cancellationToken);
    Task<Result> ProcessImport(Guid importId, CancellationToken cancellationToken);
}
