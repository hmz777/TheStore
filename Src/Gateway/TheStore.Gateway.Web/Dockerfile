FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /source
COPY ["Src/Gateway/TheStore.Gateway.Web/TheStore.Gateway.Web.csproj", "Src/Gateway/TheStore.Gateway.Web/"]
RUN dotnet restore "Src/Gateway/TheStore.Gateway.Web/TheStore.Gateway.Web.csproj"
COPY . .
WORKDIR "/source/Src/Gateway/TheStore.Gateway.Web"
RUN dotnet build "TheStore.Gateway.Web.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "TheStore.Gateway.Web.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TheStore.Gateway.Web.dll"]