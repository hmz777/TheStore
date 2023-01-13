#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["APIs/Catalog/TheStore.Catalog.API/TheStore.Catalog.API.csproj", "APIs/Catalog/TheStore.Catalog.API/"]
COPY ["Shared/TheStore.SharedModels/TheStore.SharedModels.csproj", "Shared/TheStore.SharedModels/"]
COPY ["APIs/Catalog/TheStore.Catalog.Infrastructure/TheStore.Catalog.Infrastructure.csproj", "APIs/Catalog/TheStore.Catalog.Infrastructure/"]
COPY ["Shared/TheStore.ApiCommon/TheStore.ApiCommon.csproj", "Shared/TheStore.ApiCommon/"]
COPY ["Shared/TheStore.SharedKernel/TheStore.SharedKernel.csproj", "Shared/TheStore.SharedKernel/"]
COPY ["APIs/Catalog/TheStore.Catalog.Core/TheStore.Catalog.Core.csproj", "APIs/Catalog/TheStore.Catalog.Core/"]
RUN dotnet restore "APIs/Catalog/TheStore.Catalog.API/TheStore.Catalog.API.csproj"
COPY . .
WORKDIR "/src/APIs/Catalog/TheStore.Catalog.API"
RUN dotnet build "TheStore.Catalog.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "TheStore.Catalog.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TheStore.Catalog.API.dll"]