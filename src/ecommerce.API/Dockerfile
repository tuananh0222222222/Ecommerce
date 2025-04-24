# Base image dùng để chạy
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app


EXPOSE 8080
EXPOSE 8081

# Image dùng để build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

WORKDIR /src

# Copy file csproj
COPY ["src/ecommerce.API/ecommerce.API.csproj", "ecommerce.API/"]
COPY ["src/ecommerce.application/ecommerce.application.csproj", "ecommerce.application/"]
COPY ["src/ecommerce.domain/ecommerce.domain.csproj", "ecommerce.domain/"]
COPY ["src/ecommerce.infrastructure/ecommerce.infrastructure.csproj", "ecommerce.infrastructure/"]
COPY ["src/ecommerce.persistent/ecommerce.persistence.csproj", "ecommerce.persistent/"]

# Restore
RUN dotnet restore "ecommerce.API/ecommerce.API.csproj"

# Copy toàn bộ code
COPY . .

# Build
WORKDIR "src/ecommerce.API"
RUN dotnet build "ecommerce.API.csproj" -c Release -o /app/build

# Publish
FROM build AS publish

RUN dotnet publish "ecommerce.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Final runtime
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ecommerce.API.dll"]
