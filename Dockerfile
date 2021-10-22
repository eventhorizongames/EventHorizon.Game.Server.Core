# Stage - Build
FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /source

# Copy the solution file
COPY ./*.sln ./

# Copy the main source project files
COPY src/*/*.csproj ./
RUN for file in $(ls *.csproj); do mkdir -p src/${file%.*}/ && mv $file src/${file%.*}/; done

# Copy the test project files
COPY test/*/*.csproj ./
RUN for file in $(ls *.csproj); do mkdir -p test/${file%.*}/ && mv $file test/${file%.*}/; done

RUN dotnet restore

# copy and build everything else
COPY src/. ./src/
COPY test/. ./test/

RUN dotnet build

# Stage - publish
FROM build AS publish
WORKDIR /source
RUN dotnet publish --output /app/ --configuration Release --no-restore ./src/EventHorizon.Game.Server.Core/EventHorizon.Game.Server.Core.csproj

# Stage - runtime
FROM mcr.microsoft.com/dotnet/core/aspnet:5.0 AS runtime
ARG BUILD_VERSION=0.0.0
ENV APPLICATION_VERSION=$BUILD_VERSION

WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "EventHorizon.Game.Server.Core.dll"]