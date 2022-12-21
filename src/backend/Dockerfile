FROM mcr.microsoft.com/dotnet/sdk:7.0 AS base

WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS publish

WORKDIR /app

COPY ["src/backend/.", "."]

RUN dotnet restore

COPY . .

RUN dotnet publish "/app/WebApi/WebApi.csproj" -c Release -o /app/publish

FROM publish AS test

WORKDIR /app

ENTRYPOINT ["dotnet", "test"]

FROM build AS publish

RUN dotnet publish -c release --no-build -o /app

FROM  base AS final

WORKDIR /app

COPY --from=publish /app/publish .

ENV ASPNETCORE_URLS=http://+:5000

EXPOSE 5000

ENTRYPOINT ["dotnet", "WebApi.dll"]