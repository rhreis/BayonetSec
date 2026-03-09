# Use the official ASP.NET Core runtime as a base image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

# Use the SDK image to build the app
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy csproj files and restore dependencies
COPY ["backend/BayonetSec.Api/BayonetSec.Api.csproj", "backend/BayonetSec.Api/"]
COPY ["backend/BayonetSec.Application/BayonetSec.Application.csproj", "backend/BayonetSec.Application/"]
COPY ["backend/BayonetSec.Domain/BayonetSec.Domain.csproj", "backend/BayonetSec.Domain/"]
COPY ["backend/BayonetSec.Infrastructure/BayonetSec.Infrastructure.csproj", "backend/BayonetSec.Infrastructure/"]
RUN dotnet restore "backend/BayonetSec.Api/BayonetSec.Api.csproj"

# Copy everything else and build
COPY . .
WORKDIR "/src/backend/BayonetSec.Api"
RUN dotnet build "BayonetSec.Api.csproj" -c Release -o /app/build

# Publish the app
FROM build AS publish
RUN dotnet publish "BayonetSec.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Final stage
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BayonetSec.Api.dll"]