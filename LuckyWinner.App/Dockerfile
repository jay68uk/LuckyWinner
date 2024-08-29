FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["LuckyWinner.App/LuckyWinner.App.csproj", "LuckyWinner.App/"]
RUN dotnet restore "LuckyWinner.App/LuckyWinner.App.csproj"
COPY . .
WORKDIR "/src/LuckyWinner.App"
RUN dotnet build "LuckyWinner.App.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "LuckyWinner.App.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "LuckyWinner.App.dll"]
