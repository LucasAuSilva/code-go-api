# Build
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /source
COPY . ./
EXPOSE 80
EXPOSE 443
EXPOSE 3000

# Setup For apply migrations
RUN dotnet tool install --global dotnet-ef
ENV PATH="$PATH:/root/.dotnet/tools"
ENV PORT=3000

RUN dotnet restore
RUN dotnet publish -c release -o /app --no-restore

# Run
FROM mcr.microsoft.com/dotnet/aspnet:7.0
WORKDIR /app
COPY --from=build /app ./

ENTRYPOINT ["dotnet", "CodeGo.Api.dll"]
