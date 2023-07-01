FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base

WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["src/CleanArchitecture.Web/CleanArchitecture.Web.csproj", "src/CleanArchitecture.Web/"]
RUN dotnet restore "src/CleanArchitecture.Web/CleanArchitecture.Web.csproj"
COPY . .
WORKDIR "/src/src/CleanArchitecture.Web"
RUN dotnet build "CleanArchitecture.Web.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CleanArchitecture.Web.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CleanArchitecture.Web.dll"]
