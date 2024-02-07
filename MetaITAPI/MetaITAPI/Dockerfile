FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src

COPY ./MetaITAPI.csproj ./
RUN dotnet restore "MetaITAPI.csproj"

COPY ["./", "./"]
RUN dotnet build "MetaITAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MetaITAPI.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

RUN apt-get update && apt-get install -y postgresql-client
ENV CONNECTION_STRING="Host=localhost;Database=postgres;Username=postgres;Password=postgres"

ENTRYPOINT ["dotnet", "MetaITAPI.dll"]
