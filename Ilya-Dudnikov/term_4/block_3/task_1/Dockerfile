FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["ExamSystemWebAPI/ExamSystemWebAPI.csproj", "ExamSystemWebAPI/"]
COPY ["ExamSystem/ExamSystem.csproj", "ExamSystem/"]
RUN dotnet restore "ExamSystemWebAPI/ExamSystemWebAPI.csproj"
COPY . .
WORKDIR "/src/ExamSystemWebAPI"
RUN dotnet build "ExamSystemWebAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ExamSystemWebAPI.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ExamSystemWebAPI.dll"]

