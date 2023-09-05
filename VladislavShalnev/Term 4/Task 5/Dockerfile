FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Deanery.API/Deanery.API.csproj", "Deanery.API/"]
RUN dotnet restore "Deanery.API/Deanery.API.csproj"
COPY . .
WORKDIR "/src/Deanery.API"
RUN dotnet build "Deanery.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Deanery.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Deanery.API.dll"]
