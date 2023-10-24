FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
#EXPOSE 5218

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Book-store-api/BookstoreAPI.csproj", "Book-store-api/"]
RUN dotnet restore "BookstoreAPI/BookstoreAPI.csproj"
COPY . .
WORKDIR "/src/BookstoreAPI"
RUN dotnet build "BookstoreAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "BookstoreAPI.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BookstoreAPI.dll"]
