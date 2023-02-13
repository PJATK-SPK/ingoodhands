FROM mcr.microsoft.com/dotnet/sdk:7.0 AS base

WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS publish
ARG APP
ENV APP=$APP
WORKDIR /app

COPY ["src/backend/.", "."]

RUN dotnet restore

COPY . .

RUN dotnet publish "/app/${APP}/${APP}.csproj" -c Release -o /app/publish

FROM publish AS test

WORKDIR /app

ENTRYPOINT ["dotnet", "test"]

FROM  base AS final
ARG APP
ENV APP=$APP
WORKDIR /app

COPY --from=publish /app/publish .

ENV ASPNETCORE_URLS=http://+:5000

EXPOSE 5000

ENTRYPOINT ["dotnet", "${APP}.dll"]
