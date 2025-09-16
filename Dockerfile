FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["myfinance-web-dotnet.csproj", "./"]
COPY ["myfinance-web-dotnet-service/myfinance-web-dotnet-service.csproj", "myfinance-web-dotnet-service/"]
COPY ["myfinance-web-dotnet-domain/myfinance-web-dotnet-domain.csproj", "myfinance-web-dotnet-domain/"]
COPY ["myfinance-web-dotnet-infra/myfinance-web-dotnet-infra.csproj", "myfinance-web-dotnet-infra/"]
RUN dotnet restore "myfinance-web-dotnet.csproj"
COPY . .
WORKDIR "/src/"
RUN dotnet build "./myfinance-web-dotnet.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./myfinance-web-dotnet.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "myfinance-web-dotnet.dll"]
