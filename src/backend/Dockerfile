FROM mcr.microsoft.com/dotnet/sdk:6.0 AS base

WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build

WORKDIR /src

COPY ["src/backend/WebApi/WebApi.csproj", "src/backend/WebApi/"]

RUN dotnet restore "src/backend/WebApi/WebApi.csproj"

COPY . .

WORKDIR "/src/src/backend/WebApi"
RUN dotnet build "WebApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "WebApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
EXPOSE 5000
ENTRYPOINT ["dotnet", "WebApi.dll"]