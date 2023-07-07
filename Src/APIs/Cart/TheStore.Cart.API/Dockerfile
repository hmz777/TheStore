FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /source
COPY ["Src/APIs/Cart/TheStore.Cart.API/TheStore.Cart.API.csproj", "Src/APIs/Cart/TheStore.Cart.API/"]
COPY ["Src/APIs/Cart/TheStore.Cart.Infrastructure/TheStore.Cart.Infrastructure.csproj", "Src/APIs/Cart/TheStore.Cart.Infrastructure/"]
COPY ["Src/Shared/TheStore.ApiCommon/TheStore.ApiCommon.csproj", "Src/Shared/TheStore.ApiCommon/"]
COPY ["Src/Shared/TheStore.SharedKernel/TheStore.SharedKernel.csproj", "Src/Shared/TheStore.SharedKernel/"]
COPY ["Src/Shared/TheStore.SharedModels/TheStore.SharedModels.csproj", "Src/Shared/TheStore.SharedModels/"]
COPY ["Src/APIs/Cart/TheStore.Cart.Core/TheStore.Cart.Core.csproj", "Src/APIs/Cart/TheStore.Cart.Core/"]
RUN dotnet restore "Src/APIs/Cart/TheStore.Cart.API/TheStore.Cart.API.csproj"
COPY . .
WORKDIR "/source/Src/APIs/Cart/TheStore.Cart.API"
RUN dotnet build "TheStore.Cart.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "TheStore.Cart.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TheStore.Cart.API.dll"]