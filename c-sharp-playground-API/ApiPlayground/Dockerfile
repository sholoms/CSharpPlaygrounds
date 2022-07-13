FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build-env
WORKDIR /ApiPlayground

# Copy everything
COPY . ./
# Restore as distinct layers
# RUN dotnet restore
# Build and publish a release

RUN dotnet publish -c Release -o out

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /ApiPlayground
COPY --from=build-env /ApiPlayground/out .
ENV dockertest 5-5
ENTRYPOINT ["dotnet", "ApiPlayground.dll"]
