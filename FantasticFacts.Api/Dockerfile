FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["FantasticFacts.Api/FantasticFacts.Api.csproj", "FantasticFacts.Api/"]
RUN dotnet restore "FantasticFacts.Api/FantasticFacts.Api.csproj"
COPY . .
WORKDIR "/src/FantasticFacts.Api"
RUN dotnet build "./FantasticFacts.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./FantasticFacts.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "FantasticFacts.Api.dll"]
