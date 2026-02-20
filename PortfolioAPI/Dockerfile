FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY ["PortfolioAPI/PortfolioAPI.csproj", "PortfolioAPI/"]
COPY ["PortfolioAPI.Models/PortfolioAPI.Models.csproj", "PortfolioAPI.Models/"]
COPY ["PortfolioAPI.Services/PortfolioAPI.Services.csproj", "PortfolioAPI.Services/"]

RUN dotnet restore "PortfolioAPI/PortfolioAPI.csproj"

COPY . .
WORKDIR "/src/PortfolioAPI"
RUN dotnet publish "PortfolioAPI.csproj" -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "PortfolioAPI.dll"]