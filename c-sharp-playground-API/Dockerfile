FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build-env
WORKDIR /ApiPlayground

# Copy everything
COPY ./ApiPlayground.Endpoint ./ApiPlayground.Endpoint
COPY ./DbPlayground.Persistence ./DbPlayground.Persistence
# Restore as distinct layers
# RUN dotnet restore
# Build and publish a release

RUN dotnet publish ./ApiPlayground.Endpoint -c Release -o out

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /app
COPY --from=build-env /ApiPlayground/out .
ENV filepath /app/test.txt
ENTRYPOINT ["dotnet", "ApiPlayground.Endpoint.dll"]
