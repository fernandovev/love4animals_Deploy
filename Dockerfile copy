FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

COPY Love4AnimalsApi/Love4AnimalsApi.csproj Love4AnimalsApi/
RUN dotnet restore Love4AnimalsApi/Love4AnimalsApi.csproj

COPY . .
RUN dotnet publish Love4AnimalsApi/Love4AnimalsApi.csproj -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS final
WORKDIR /app

COPY --from=build /app/publish .

ENV ASPNETCORE_URLS=http://0.0.0.0:${PORT}

ENTRYPOINT ["dotnet", "Love4AnimalsApi.dll"]