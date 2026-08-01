## Build stage
#FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
#ARG BUILD_CONFIGURATION=Release
#WORKDIR /src
#
## Copy everything
#COPY . .
#
## Restore
#RUN dotnet restore "./SampleApi.csproj"
#
## Build
#RUN dotnet build "./SampleApi.csproj" -c $BUILD_CONFIGURATION -o /app/build
#
## Publish
#RUN dotnet publish "./SampleApi.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false
#
## Runtime stage
#FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
#WORKDIR /app
#COPY --from=build /app/publish .
#
#ENV ASPNETCORE_URLS=http://+:8080
#EXPOSE 8080
#
#ENTRYPOINT ["dotnet", "SampleApi.dll"]
# Build stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release

# Set working directory inside the container
WORKDIR /src

# Copy only the csproj first (better caching)
COPY SampleApi/SampleApi.csproj SampleApi/

# Restore dependencies
# RUN dotnet restore "SampleApi/SampleApi.csproj" 
# Get all the restore packages from the nuget cache in my local machine
# this solution is only when running in public networks (hotspots)
# also disable running jobs in parallel
#COPY nuget.config .
#RUN dotnet restore "SampleApi/SampleApi.csproj" --disable-parallel
RUN --mount=type=cache,target=/root/.nuget/packages \
    dotnet restore "SampleApi/SampleApi.csproj" --disable-parallel



# Copy the rest of the source code
COPY SampleApi/ SampleApi/

# Build
RUN dotnet build "SampleApi/SampleApi.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Publish
RUN dotnet publish "SampleApi/SampleApi.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app

COPY --from=build /app/publish .

ENV ASPNETCORE_URLS=http://+:8080
EXPOSE 8080

ENTRYPOINT ["dotnet", "SampleApi.dll"]
