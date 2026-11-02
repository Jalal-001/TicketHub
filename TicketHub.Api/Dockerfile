#Stage1:Build and Publish
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

#copy csproj and restore dependencies
COPY ["TicketHub.Api/TicketHub.Api.csproj", "TicketHub.Api/"]
COPY ["TicketHub.Domain/TicketHub.Domain.csproj", "TicketHub.Domain/"] 
COPY ["TicketHub.Application/TicketHub.Application.csproj", "TicketHub.Application/"]
COPY ["TicketHub.Infrastructure/TicketHub.Infrastructure.csproj", "TicketHub.Infrastructure/"]
RUN dotnet restore "TicketHub.Api/TicketHub.Api.csproj"

COPY . .
WORKDIR "/src/TicketHub.Api"
RUN dotnet publish "TicketHub.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

#Stage2:Publish (Runtime)
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .

ENTRYPOINT ["dotnet", "TicketHub.Api.dll"]	