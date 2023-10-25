#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["UnitOfWorkPJEx_Dapper2/UnitOfWorkPJEx_Dapper2.csproj", "UnitOfWorkPJEx_Dapper2/"]
RUN dotnet restore "UnitOfWorkPJEx_Dapper2/UnitOfWorkPJEx_Dapper2.csproj"
COPY . .
WORKDIR "/src/UnitOfWorkPJEx_Dapper2"
RUN dotnet build "UnitOfWorkPJEx_Dapper2.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "UnitOfWorkPJEx_Dapper2.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "UnitOfWorkPJEx_Dapper2.dll"]