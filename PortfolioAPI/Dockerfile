FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY ["PortfolioAPI/PortfolioAPI.csproj", "PortfolioAPI/"]
COPY ["Models/PortfolioAPI.Models.csproj", "Models/"]
COPY ["Services/PortfolioAPI.Services.csproj", "Services/"]

RUN dotnet restore "PortfolioAPI/PortfolioAPI.csproj"

COPY . .
WORKDIR "/src/PortfolioAPI"
RUN dotnet publish -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/publish .

ENTRYPOINT ["dotnet", "PortfolioAPI.dll"]