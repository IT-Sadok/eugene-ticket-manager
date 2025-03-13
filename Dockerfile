FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["EventService/EventService.Api/EventService.Api.csproj", "EventService/EventService.Api/"]
RUN dotnet restore "EventService/EventService.Api/EventService.Api.csproj"
COPY . .
WORKDIR "/src/EventService/EventService.Api"
RUN dotnet build "EventService.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "EventService.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
ENV ASPNETCORE_URLS=http://+:80
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "EventService.Api.dll"]