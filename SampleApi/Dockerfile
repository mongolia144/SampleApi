# Build stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# Copy everything
COPY . .

# Restore
RUN dotnet restore "./SampleApi.csproj"

# Build
RUN dotnet build "./SampleApi.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Publish
RUN dotnet publish "./SampleApi.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=build /app/publish .

# IMPORTANT:
# Azure Container Apps *requires* the application to LISTEN on port 8080 INSIDE the container.
# ASP.NET Core normally listens on ports 5000/5001 unless overridden.
# Without this line, the app will NOT bind to 8080 → ACA will crash the container (CrashLoopBackOff).
ENV ASPNETCORE_URLS=http://+:8080

# This exposes port 8080 externally, but does NOT make ASP.NET Core listen on it.
# The ENV line above is what actually forces ASP.NET Core to bind to 8080.
EXPOSE 8080

ENTRYPOINT ["dotnet", "SampleApi.dll"]
