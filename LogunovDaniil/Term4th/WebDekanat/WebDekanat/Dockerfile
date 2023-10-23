#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

#Depending on the operating system of the host machines(s) that will build or run the containers, the image specified in the FROM statement may need to be changed.
#For more information, please see https://aka.ms/containercompat

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["WebDekanat/WebDekanat.csproj", "WebDekanat/"]
COPY ["WebDekanat.Containers/WebDekanat.Containers.csproj", "WebDekanat.Containers/"]
RUN dotnet restore "WebDekanat/WebDekanat.csproj"
COPY . .
WORKDIR "/src/WebDekanat"
RUN dotnet build "WebDekanat.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "WebDekanat.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "WebDekanat.dll"]