# --- Build stage ---
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# Copy solution file
COPY FoodDist.sln ./

# Copy project files with correct relative paths
COPY src/FoodDist.Api/FoodDist.Api.csproj src/FoodDist.Api/
COPY src/FoodDist.Core/FoodDist.Core.csproj src/FoodDist.Core/
COPY src/FoodDist.Infrastructure/FoodDist.Infrastructure.csproj src/FoodDist.Infrastructure/

# Restore dependencies
RUN dotnet restore FoodDist.sln

# Copy the rest of the source code
COPY . .

# Build & publish API
WORKDIR /src/src/FoodDist.Api
RUN dotnet publish -c Release -o /app --no-restore

# --- Runtime stage ---
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS runtime
WORKDIR /app
COPY --from=build /app ./
ENTRYPOINT ["dotnet", "FoodDist.Api.dll"]
