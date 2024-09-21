#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/azure-functions/dotnet-isolated:4-dotnet-isolated8.0 AS base
WORKDIR /home/site/wwwroot
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Inventory/StileStream.Wms.Inventory.FunctionApp/StileStream.Wms.Inventory.FunctionApp.csproj", "Inventory/StileStream.Wms.Inventory.FunctionApp/"]
COPY ["Inventory/StileStream.Wms.Inventory.Application/StileStream.Wms.Inventory.Application.csproj", "Inventory/StileStream.Wms.Inventory.Application/"]
COPY ["SharedKernel/StileStream.Wms.SharedKernel.Application/StileStream.Wms.SharedKernel.Application.csproj", "SharedKernel/StileStream.Wms.SharedKernel.Application/"]
COPY ["SharedKernel/StileStream.Wms.SharedKernel.Domain/StileStream.Wms.SharedKernel.Domain.csproj", "SharedKernel/StileStream.Wms.SharedKernel.Domain/"]
COPY ["Inventory/StileStream.Wms.Inventory.Domain/StileStream.Wms.Inventory.Domain.csproj", "Inventory/StileStream.Wms.Inventory.Domain/"]
COPY ["Inventory/StileStream.Wms.Inventory.Infrastructure/StileStream.Wms.Inventory.Infrastructure.Data.csproj", "Inventory/StileStream.Wms.Inventory.Infrastructure.Data/"]
RUN dotnet restore "./Inventory/StileStream.Wms.Inventory.FunctionApp/StileStream.Wms.Inventory.FunctionApp.csproj"
COPY . .
WORKDIR "/src/Inventory/StileStream.Wms.Inventory.FunctionApp"
RUN dotnet build "./StileStream.Wms.Inventory.FunctionApp.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./StileStream.Wms.Inventory.FunctionApp.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /home/site/wwwroot
COPY --from=publish /app/publish .
ENV AzureWebJobsScriptRoot=/home/site/wwwroot \
    AzureFunctionsJobHost__Logging__Console__IsEnabled=true