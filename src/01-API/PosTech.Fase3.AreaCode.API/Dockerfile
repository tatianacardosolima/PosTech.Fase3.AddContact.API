FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8081

ENV ASPNETCORE_URLS=http://+:8081
ENV ASPNETCORE_HTTP_PORTS=8081

USER app
FROM --platform=$BUILDPLATFORM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG configuration=Release
WORKDIR /src
COPY ["src/01-API/PosTech.Fase3.AreaCode.API/PosTech.Fase3.AreaCode.API.csproj", "src/01-API/PosTech.Fase3.AreaCode.API/"]
RUN dotnet restore "src/01-API/PosTech.Fase3.AreaCode.API/PosTech.Fase3.AreaCode.API.csproj"
COPY . .
WORKDIR "/src/src/01-API/PosTech.Fase3.AreaCode.API"
RUN dotnet build "PosTech.Fase3.AreaCode.API.csproj" -c $configuration -o /app/build

FROM build AS publish
ARG configuration=Release
RUN dotnet publish "PosTech.Fase3.AreaCode.API.csproj" -c $configuration -o /app/publish /p:UseAppHost=false

FROM build AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PosTech.Fase3.AreaCode.API.dll"]