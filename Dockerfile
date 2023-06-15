FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["IoDit.WebAPI/IoDit.WebAPI.csproj", "IoDit.WebAPI/"]
RUN dotnet restore "IoDit.WebAPI/IoDit.WebAPI.csproj"
COPY . .
WORKDIR "/src/IoDit.WebAPI"
RUN dotnet build "IoDit.WebAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "IoDit.WebAPI.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "IoDit.WebAPI.dll"]
