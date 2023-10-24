# Use the aspnet image as the base image for runtime environment
FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 5218  # Expose the port your application will listen on

# Use the sdk image as the build environment
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src

# Copy the .csproj file and restore any dependencies (if needed)
COPY ["BookstoreAPI/BookstoreAPI.csproj", "BookstoreAPI/"]
RUN dotnet restore "BookstoreAPI/BookstoreAPI.csproj"

# Copy the rest of the application and build it
COPY . .
WORKDIR "/src/BookstoreAPI"
RUN dotnet build "BookstoreAPI.csproj" -c Release -o /app/build

# Publish the application
FROM build AS publish
RUN dotnet publish "BookstoreAPI.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Use the base image for the final stage
FROM base AS final
WORKDIR /app

# Copy the published application to the final image
COPY --from=publish /app/publish .

# Set the entry point for the application
ENTRYPOINT ["dotnet", "BookstoreAPI.dll"]
