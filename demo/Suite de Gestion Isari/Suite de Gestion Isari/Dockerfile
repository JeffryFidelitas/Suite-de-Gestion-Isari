# Base image for ASP.NET Core Runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80

# Base image for SDK to build and publish the app
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY . .
RUN dotnet publish -c Release -o /app/publish

# Final image
FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .
COPY EnvioFactura.html /app/
COPY RecuperarAcceso.html /app/
ENTRYPOINT ["dotnet", "Suite de Gestion Isari.dll"]

