# Stage - Build
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /source

# Copy the solution file
COPY ./*.sln ./

# Copy the main source project files
COPY src/EventHorizon.Game.Server.Core/*.csproj ./src/EventHorizon.Game.Server.Core/
COPY src/EventHorizon.Identity/*.csproj ./src/EventHorizon.Identity/
COPY src/EventHorizon.Platform.Integration/*.csproj ./src/EventHorizon.Platform.Integration/
COPY src/EventHorizon.TimerService/*.csproj ./src/EventHorizon.TimerService/

# Copy the test project files
COPY test/EventHorizon.Game.Server.Core.Tests/*.csproj ./test/EventHorizon.Game.Server.Core.Tests/

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
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS runtime
ARG BUILD_VERSION=0.0.0
ENV APPLICATION_VERSION=$BUILD_VERSION

WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "EventHorizon.Game.Server.Core.dll"]