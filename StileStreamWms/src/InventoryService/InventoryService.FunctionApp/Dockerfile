#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/azure-functions/dotnet-isolated:4-dotnet-isolated8.0 AS base
WORKDIR /home/site/wwwroot
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/InventoryService.FunctionApp/InventoryService.FunctionApp.csproj", "src/InventoryService.FunctionApp/"]
COPY ["src/InventoryService.Infrastructure/InventoryService.Infrastructure.csproj", "src/InventoryService.Infrastructure/"]
COPY ["src/InventoryService.Domain/InventoryService.Domain.csproj", "src/InventoryService.Domain/"]
COPY ["src/Shared.Domain/Shared.Domain.csproj", "src/Shared.Domain/"]
RUN dotnet restore "./src/InventoryService.FunctionApp/InventoryService.FunctionApp.csproj"
COPY . .
WORKDIR "/src/src/InventoryService.FunctionApp"
RUN dotnet build "./InventoryService.FunctionApp.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./InventoryService.FunctionApp.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /home/site/wwwroot
COPY --from=publish /app/publish .
ENV AzureWebJobsScriptRoot=/home/site/wwwroot \
    AzureFunctionsJobHost__Logging__Console__IsEnabled=true