FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copiar projetos e restaurar dependências
COPY ["BlazorWeatherApp.Api/BlazorWeatherApp.Api.csproj", "BlazorWeatherApp.Api/"]
RUN dotnet restore "BlazorWeatherApp.Api/BlazorWeatherApp.Api.csproj"

# Copiar o resto do código e fazer o build
COPY . .
WORKDIR "/src/BlazorWeatherApp.Api"
RUN dotnet build "BlazorWeatherApp.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "BlazorWeatherApp.Api.csproj" -c Release -o /app/publish

# Construir a imagem final
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BlazorWeatherApp.Api.dll"]
