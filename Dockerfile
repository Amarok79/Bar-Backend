FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["nuget.config", "."]
COPY ["src/Bar.Backend/Bar.Backend.csproj", "src/Bar.Backend/"]
COPY ["src/Bar.Data/Bar.Data.csproj", "src/Bar.Data/"]
COPY ["src/Bar.Domain/Bar.Domain.csproj", "src/Bar.Domain/"]
RUN dotnet restore "src/Bar.Backend/Bar.Backend.csproj"
COPY . .
WORKDIR "/src/src/Bar.Backend"
RUN dotnet build "Bar.Backend.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Bar.Backend.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Bar.Backend.dll"]
