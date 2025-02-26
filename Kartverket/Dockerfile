# Use a Linux-based ASP.NET runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

# Use a Linux-based SDK image for building
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Kartverket/Kartverket.csproj", "Kartverket/"]
RUN dotnet restore "./Kartverket/Kartverket.csproj"
COPY . .
WORKDIR "/src/Kartverket"
RUN dotnet build "./Kartverket.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Publish the application
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Kartverket.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Use the Linux-based runtime image for the final stage
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Kartverket.dll"]