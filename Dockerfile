FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS base
WORKDIR /app
EXPOSE 8080

ENV ASPNETCORE_URLS=http://+:8080

USER app

# Build the React client into static files (client/dist)
FROM --platform=$BUILDPLATFORM node:22-alpine AS client
WORKDIR /client
COPY client/package.json client/package-lock.json ./
RUN npm ci
COPY client/ ./
RUN npm run build

FROM --platform=$BUILDPLATFORM mcr.microsoft.com/dotnet/sdk:10.0 AS build
ARG configuration=Release
WORKDIR /src
COPY ["AirportApi/AirportApi.csproj", "AirportApi/"]
RUN dotnet restore "AirportApi/AirportApi.csproj"
COPY AirportApi/ AirportApi/
WORKDIR /src/AirportApi
RUN dotnet build "AirportApi.csproj" -c $configuration -o /app/build

FROM build AS publish
ARG configuration=Release
RUN dotnet publish "AirportApi.csproj" -c $configuration -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
# The API serves the React build from wwwroot (see Program.cs)
COPY --from=client /client/dist ./wwwroot
ENTRYPOINT ["dotnet", "AirportApi.dll"]
