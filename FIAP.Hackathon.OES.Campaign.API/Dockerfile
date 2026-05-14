# Build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

COPY ["FIAP.Hackathon.OES.Campaign.Domain/FIAP.Hackathon.OES.Campaign.Domain.csproj", "FIAP.Hackathon.OES.Campaign.Domain/"]
COPY ["FIAP.Hackathon.OES.Campaign.Infra/FIAP.Hackathon.OES.Campaign.Infra.csproj", "FIAP.Hackathon.OES.Campaign.Infra/"]
COPY ["FIAP.Hackathon.OES.Campaign.Service/FIAP.Hackathon.OES.Campaign.Service.csproj", "FIAP.Hackathon.OES.Campaign.Service/"]
COPY ["FIAP.Hackathon.OES.Campaign.API/FIAP.Hackathon.OES.Campaign.API.csproj", "FIAP.Hackathon.OES.Campaign.API/"]

RUN dotnet restore "FIAP.Hackathon.OES.Campaign.API/FIAP.Hackathon.OES.Campaign.API.csproj"

COPY . .

WORKDIR "/app/FIAP.Hackathon.OES.Campaign.API"
RUN dotnet publish -c Release -o /app/publish /p:UseAppHost=false

# Runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0-alpine AS runtime

RUN addgroup -S appgroup && adduser -S appuser -G appgroup

WORKDIR /app
COPY --from=build /app/publish .

ENV ASPNETCORE_URLS=http://+:3002
ENV ASPNETCORE_ENVIRONMENT=Production

EXPOSE 3002

HEALTHCHECK --interval=30s --timeout=5s --retries=3 \
  CMD wget -qO- http://localhost:3002/health || exit 1

USER appuser

ENTRYPOINT ["dotnet", "FIAP.Hackathon.OES.Campaign.API.dll"]