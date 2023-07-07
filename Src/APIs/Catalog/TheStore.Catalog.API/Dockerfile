FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /source
COPY ["Src/APIs/Catalog/TheStore.Catalog.API/TheStore.Catalog.API.csproj", "Src/APIs/Catalog/TheStore.Catalog.API/"]
COPY ["Src/APIs/Catalog/TheStore.Catalog.Infrastructure/TheStore.Catalog.Infrastructure.csproj", "Src/APIs/Catalog/TheStore.Catalog.Infrastructure/"]
COPY ["Src/Shared/TheStore.ApiCommon/TheStore.ApiCommon.csproj", "Src/Shared/TheStore.ApiCommon/"]
COPY ["Src/Shared/TheStore.SharedKernel/TheStore.SharedKernel.csproj", "Src/Shared/TheStore.SharedKernel/"]
COPY ["Src/Shared/TheStore.SharedModels/TheStore.SharedModels.csproj", "Src/Shared/TheStore.SharedModels/"]
COPY ["Src/APIs/Catalog/TheStore.Catalog.Core/TheStore.Catalog.Core.csproj", "Src/APIs/Catalog/TheStore.Catalog.Core/"]
RUN dotnet restore "Src/APIs/Catalog/TheStore.Catalog.API/TheStore.Catalog.API.csproj"
COPY . .
WORKDIR "/source/Src/APIs/Catalog/TheStore.Catalog.API"
RUN dotnet build "TheStore.Catalog.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "TheStore.Catalog.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TheStore.Catalog.API.dll"]