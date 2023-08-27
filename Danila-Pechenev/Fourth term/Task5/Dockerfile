FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY . .

RUN dotnet restore "CreditSystem/CreditSystem.csproj"
COPY . .
WORKDIR "/src/CreditSystem"
RUN dotnet build "CreditSystem.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CreditSystem.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CreditSystem.dll"]