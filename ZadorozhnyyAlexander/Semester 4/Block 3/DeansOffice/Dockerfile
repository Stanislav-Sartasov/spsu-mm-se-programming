FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["DOWEBAPI/DOWEBAPI.csproj", "DOWEBAPI/"]
RUN dotnet restore "DOWEBAPI/DOWEBAPI.csproj"
COPY . .
WORKDIR "/src/DOWEBAPI"
RUN dotnet build "DOWEBAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "DOWEBAPI.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "DOWEBAPI.dll"]