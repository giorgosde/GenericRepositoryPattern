FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build-env
WORKDIR /app

COPY . ./
WORKDIR /app/GenericRepository.Api
RUN dotnet restore

RUN dotnet publish -c Release -o out GenericRepository.Api.csproj

FROM mcr.microsoft.com/dotnet/aspnet:7.0
WORKDIR /app
COPY --from=build-env /app/GenericRepository.Api/out .
ENTRYPOINT ["dotnet", "GenericRepository.Api.dll"]
