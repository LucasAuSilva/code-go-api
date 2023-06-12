# Build
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /source
COPY . ./

RUN dotnet restore
RUN dotnet publish -c release -o /app --no-restore

# Run
FROM mcr.microsoft.com/dotnet/aspnet:7.0
WORKDIR /app
COPY --from=build /app ./

EXPOSE 5000
CMD []"dotnet", "CodeGo.Api.dll"]

