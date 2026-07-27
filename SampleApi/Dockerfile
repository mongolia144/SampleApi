# Build stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# Copy only the csproj first (VS-style)
COPY ["SampleApi/SampleApi.csproj", "SampleApi/"]
RUN dotnet restore "SampleApi/SampleApi.csproj"

# Copy the entire repo
COPY . .

# Build the project
WORKDIR "/src/SampleApi"
RUN dotnet build "SampleApi.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Publish stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "SampleApi.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=publish /app/publish .
EXPOSE 8080
ENTRYPOINT ["dotnet", "SampleApi.dll"]
