FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["ASP.NETCore-API-REST-Template.csproj", ""]
RUN dotnet restore "./ASP.NETCore-API-REST-Template.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "ASP.NETCore-API-REST-Template.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ASP.NETCore-API-REST-Template.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ASP.NETCore-API-REST-Template.dll"]