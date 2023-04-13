FROM mcr.microsoft.com/dotnet/runtime:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["JWT.Authentication.And.API.Callers/JWT.Authentication.And.API.Callers.csproj", "JWT.Authentication.And.API.Callers/"]
COPY ["Contracts/Contracts.csproj", "Contracts/"]
RUN dotnet restore "JWT.Authentication.And.API.Callers/JWT.Authentication.And.API.Callers.csproj"
COPY . .
WORKDIR "/src/JWT.Authentication.And.API.Callers"
RUN dotnet build "JWT.Authentication.And.API.Callers.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "JWT.Authentication.And.API.Callers.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "JWT.Authentication.And.API.Callers.dll"]
