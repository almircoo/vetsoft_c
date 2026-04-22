
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build

WORKDIR /src

COPY ["proyectoApiC#.csproj", "./"]

RUN dotnet restore "proyectoApiC#.csproj"

COPY . .

RUN dotnet build "proyectoApiC#.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "proyectoApiC#.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:9.0

RUN apt-get update && apt-get install -y curl && rm -rf /var/lib/apt/lists/*

WORKDIR /app

COPY --from=publish /app/publish .

RUN useradd -m -u 1000 appuser && chown -R appuser:appuser /app
USER appuser

EXPOSE 8080

ENV ASPNETCORE_URLS=http://+:8080
ENV ASPNETCORE_ENVIRONMENT=Production
ENV DOTNET_TieredCompilation=1
ENV DOTNET_TieredCompilationQuickJit=1

HEALTHCHECK --interval=30s --timeout=3s --start-period=5s --retries=3 \
  CMD curl -f http://localhost:8080/health || exit 1

ENTRYPOINT ["dotnet", "proyectoApiC#.dll"]
