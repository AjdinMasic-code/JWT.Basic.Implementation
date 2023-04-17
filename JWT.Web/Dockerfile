FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["JWT.Web/JWT.Web.csproj", "JWT.Web/"]
RUN dotnet restore "JWT.Web/JWT.Web.csproj"
COPY . .
WORKDIR "/src/JWT.Web"
RUN dotnet build "JWT.Web.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "JWT.Web.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "JWT.Web.dll"]
