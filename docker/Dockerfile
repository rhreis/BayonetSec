# Use the official ASP.NET Core runtime as a base image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

# Use the SDK image to build the app
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy csproj files and restore dependencies
COPY ["backend/ScarletSec.Api/ScarletSec.Api.csproj", "backend/ScarletSec.Api/"]
COPY ["backend/ScarletSec.Application/ScarletSec.Application.csproj", "backend/ScarletSec.Application/"]
COPY ["backend/ScarletSec.Domain/ScarletSec.Domain.csproj", "backend/ScarletSec.Domain/"]
COPY ["backend/ScarletSec.Infrastructure/ScarletSec.Infrastructure.csproj", "backend/ScarletSec.Infrastructure/"]
RUN dotnet restore "backend/ScarletSec.Api/ScarletSec.Api.csproj"

# Copy everything else and build
COPY . .
WORKDIR "/src/backend/ScarletSec.Api"
RUN dotnet build "ScarletSec.Api.csproj" -c Release -o /app/build

# Publish the app
FROM build AS publish
RUN dotnet publish "ScarletSec.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Final stage
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ScarletSec.Api.dll"]