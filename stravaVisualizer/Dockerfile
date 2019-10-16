FROM mcr.microsoft.com/dotnet/core/aspnet:2.2-stretch-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:2.2-stretch AS build
WORKDIR /src
COPY ["stravaVisualizer/StravaVisualizer.csproj", "stravaVisualizer/"]
COPY ["stravaApi/IO.Swagger/IO.Swagger.csproj", "stravaApi/IO.Swagger/"]
RUN dotnet restore "stravaVisualizer/StravaVisualizer.csproj"
COPY . .
WORKDIR "/src/stravaVisualizer"
RUN dotnet build "StravaVisualizer.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "StravaVisualizer.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "StravaVisualizer.dll"]