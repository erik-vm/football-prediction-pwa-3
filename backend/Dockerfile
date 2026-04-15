# Build stage
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# Copy csproj files and restore (layer-cached)
COPY src/FootballPrediction.Domain/FootballPrediction.Domain.csproj src/FootballPrediction.Domain/
COPY src/FootballPrediction.Application/FootballPrediction.Application.csproj src/FootballPrediction.Application/
COPY src/FootballPrediction.Infrastructure/FootballPrediction.Infrastructure.csproj src/FootballPrediction.Infrastructure/
COPY src/FootballPrediction.Api/FootballPrediction.Api.csproj src/FootballPrediction.Api/

RUN dotnet restore src/FootballPrediction.Api/FootballPrediction.Api.csproj

# Copy all source and publish
COPY src/ src/
RUN dotnet publish src/FootballPrediction.Api/FootballPrediction.Api.csproj \
    -c Release -o /app/publish

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS runtime
WORKDIR /app

COPY --from=build /app/publish .

ENV ASPNETCORE_URLS=http://+:10000
ENV ASPNETCORE_ENVIRONMENT=Production

EXPOSE 10000

ENTRYPOINT ["dotnet", "FootballPrediction.Api.dll"]
