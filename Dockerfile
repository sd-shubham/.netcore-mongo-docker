FROM mcr.microsoft.com/dotnet/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 80

ENV ASPNETCORE_URLS=http://+:80

FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
WORKDIR /src
COPY ["MongoWebApiCore/MongoWebApiCore.csproj", "MongoWebApiCore/"]
RUN dotnet restore "MongoWebApiCore/MongoWebApiCore.csproj"
COPY . .
WORKDIR "/src/MongoWebApiCore"
RUN dotnet build "MongoWebApiCore.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MongoWebApiCore.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MongoWebApiCore.dll"]
