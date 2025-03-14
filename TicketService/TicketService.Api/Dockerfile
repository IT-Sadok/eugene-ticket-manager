FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["TicketService.Api/TicketService.Api.csproj", "TicketService.Api/"]
RUN dotnet restore "TicketService.Api/TicketService.Api.csproj"
COPY . .
WORKDIR "/src/TicketService.Api"
RUN dotnet build "TicketService.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "TicketService.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
ENV ASPNETCORE_URLS=http://+:80
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TicketService.Api.dll"]