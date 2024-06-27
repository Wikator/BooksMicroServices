# Use the ASP.NET Core runtime as the base image
FROM mcr.microsoft.com/dotnet/aspnet:8.0-alpine AS base
WORKDIR /app

# Use the ASP.NET Core SDK as the build environment
FROM mcr.microsoft.com/dotnet/sdk:8.0-alpine AS build
WORKDIR /src
COPY ["./BooksServiceApi.csproj", "./"]

RUN dotnet restore "BooksServiceApi.csproj"
COPY . .

# Build the application
RUN dotnet build "BooksServiceApi.csproj" -c Release -o /app/build

# Publish the application
FROM build AS publish
RUN dotnet publish "BooksServiceApi.csproj" -c Release -o /app/publish

# Final stage: create the runtime image
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BooksServiceApi.dll"]