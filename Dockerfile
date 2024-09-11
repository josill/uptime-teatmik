FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env
WORKDIR /app

COPY ["src/UptimeTeatmik.Api/UptimeTeatmik.Api.csproj", "src/UptimeTeatmik.Api/"]
COPY ["src/UptimeTeatmik.Contracts/UptimeTeatmik.Contracts.csproj", "src/UptimeTeatmik.Contracts/"]
COPY ["src/UptimeTeatmik.Infrastructure/UptimeTeatmik.Infrastructure.csproj", "src/UptimeTeatmik.Infrastructure/"]
COPY ["src/UptimeTeatmik.Application/UptimeTeatmik.Application.csproj", "src/UptimeTeatmik.Application/"]
COPY ["src/UptimeTeatmik.Domain/UptimeTeatmik.Domain.csproj", "src/UptimeTeatmik.Domain/"]
COPY ["src/UptimeTeatmik.Tests/UptimeTeatmik.Tests.csproj", "src/UptimeTeatmik.Tests/"]

RUN dotnet restore "src/UptimeTeatmik.Api/UptimeTeatmik.Api.csproj"

COPY src/ ./src

RUN dotnet publish "src/UptimeTeatmik.Api/UptimeTeatmik.Api.csproj" -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build-env /app/out .
ENTRYPOINT ["dotnet", "UptimeTeatmik.Api.dll"]
