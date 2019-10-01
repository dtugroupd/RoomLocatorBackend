FROM microsoft/dotnet:sdk AS build-env

COPY . /app

WORKDIR /src
COPY ["RoomLocator.Api/RoomLocator.Api.csproj", "RoomLocator.Api/"]
COPY ["RoomLocator.Data/RoomLocator.Data.csproj", "RoomLocator.Data/"]
COPY ["RoomLocator.Domain/RoomLocator.Domain.csproj", "RoomLocator.Domain/"]
COPY ["Shared/Shared.csproj", "Shared/"]
COPY . .

WORKDIR /src/RoomLocator.Api
RUN dotnet publish -c Release -o /app/out


FROM microsoft/dotnet:aspnetcore-runtime
WORKDIR /app
COPY --from=build-env /app/out .
EXPOSE 80/tcp
ENTRYPOINT [ "dotnet", "RoomLocator.Api.dll" ]
