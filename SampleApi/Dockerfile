# Build stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy entire repo
COPY . .

# Restore and publish the API project explicitly
RUN dotnet restore SampleApi/SampleApi.csproj
RUN dotnet publish SampleApi/SampleApi.csproj -c Release -o /app

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app .
EXPOSE 8080
ENTRYPOINT ["dotnet", "SampleApi.dll"]
