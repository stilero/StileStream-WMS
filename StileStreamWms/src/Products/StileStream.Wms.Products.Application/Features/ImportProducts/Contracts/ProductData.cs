﻿namespace StileStream.Wms.Products.Application.Features.ImportProducts.Contracts;

public sealed record ProductData(string Name, string Sku, string Description, string Manufacturer, string Category, string Status);
