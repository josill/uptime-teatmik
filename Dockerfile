# Use the official Microsoft .NET SDK image as a build stage
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src

# Copy the project file(s) and restore dependencies
COPY ["*.csproj", "./"]
RUN dotnet restore

# Copy everything else and build the project
COPY . .
RUN dotnet publish -c Release -o /app/publish

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "UptimeTeatmik.Api.dll"]
