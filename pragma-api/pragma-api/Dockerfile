# Etapa de build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copia los archivos del proyecto y restaura dependencias
COPY . .
RUN dotnet restore

# Publica la aplicación en modo Release
RUN dotnet publish -c Release -o /app/publish

# Etapa de runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/publish .

# Expone el puerto
EXPOSE 8080

# Configura variables de entorno
ENV ASPNETCORE_URLS=http://+:8080
ENV ConnectionStrings__SqlConnectionString="Server=host.docker.internal,1433;Database=pragmadb;User Id=sa;Password=SuperSecret123!;TrustServerCertificate=True;"

# Comando de inicio
ENTRYPOINT ["dotnet", "pragma-api.dll"]