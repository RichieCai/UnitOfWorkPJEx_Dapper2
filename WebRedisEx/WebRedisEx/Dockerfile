#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["WebRedisEx/WebRedisEx.csproj", "WebRedisEx/"]
RUN dotnet restore "WebRedisEx/WebRedisEx.csproj"
COPY . .
WORKDIR "/src/WebRedisEx"
RUN dotnet build "WebRedisEx.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "WebRedisEx.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
COPY ["WebRedisEx/appsettings.json", "./"]

ENTRYPOINT ["dotnet", "WebRedisEx.dll"]