# Backend API: Campus Connect

This platform calculates and shows environmental status, activity etc. for locations at DTU Campus. This is a part of the course Software Engineering 2, Group D's solution.

This repository specifically includes the RESTful API (the backend). It is responsible for handling data, storage in the database, updating resources and authorizing the user against DTU CampusNet API.

## Execution of the Project

The backend is developed in .NET Core 2.2, so the latest of that is required. Depending on the system you are using, the setup guide might differ a bit. This guide assumes you are using Windows, as it greatly simplifies the setup.

### Prerequisites

1. Install .NET Core 2.2
2. Optionally: Install Visual Studio or Rider

### Run

To start the project either open a terminal and run `dotnet run` or open up the project in Visual Studio or a similar IDE, select `RoomLocator.Api` (see image below) and press `F5`.

### Build and Publish the Project

The web application can be built in multiple ways. We are using Docker which simplifies deployment a lot for us, though more traditional ways such as copying static files to a server is doable as well.

There are a bunch of environment variables available. Most of these can be checked out in `appsettings.json`. The most important ones are:

* `Auth:SigningKey`: The signing key for the JWT Tokens
* `AllowedHosts`: The hostnames allowed for CORS
* `ConnectionStrings:RoomLocator`: The connection string to the database
* `frontendUrl`: The URL to the frontend, i.e. <https://se2-webapp04.compute.dtu.dk>
* `Auth:CampusNet:AppName`: API Name for CampusNet
* `Auth:CampusNet:ApiToken`: API Token for CampusNet

### Docker

* Install Docker
* Login to docker hub: `docker login`
* Make sure you have access to the docker hub team
* `docker build -t dtugroupd/room-locator:latest .`
* `docker push dtugroupd/room-locator:latest`

Starting the container requires you to set a bunch of environment variables. Before starting, you need to [register at CampusNet](https://cn.inside.dtu.dk/data/Documentation/RequestApiCredentials.aspx) if you want to be able to sign in, or use our credentials, which aren't public.

Here is an example for running the container:

```bash
docker run \
   -p 5000:80 \
   -e 'ConnectionStrings__RoomLocator=Server=(localdb)\\mssqllocaldb;Database=RoomLocator;Trusted_Connection=True;MultipleActiveResultSets=true' \
   -e 'frontendUrl=http://localhost:4200' \
   -e 'Auth__CampusNet__AppName=My App Name' \
   -e 'Auth__CampusNet__ApiToken=some123-valid-token24145345' \
   -e 'ASPNETCORE_ENVIRONMENT=Release' \
   -d dtugroupd/room-locator:latest
```
