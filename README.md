# JWT.Basic.Implementation Using C#
If you are using Rider or Visual Studio you will need to run the JWT.Authentication.And.API.Callers and API projects at the same time in order to test. You can use docker to deploy both outside of IDE however, outside of the scope of this project.

The application tests a failure path (not authorized) and a success path generating a token and sending a request to protected endpoint.

Additionally if you want to get a quick run and have Docker installed. You can move into the root directory via a terminal and run the following commands:

docker-compose build

once that has completed

docker-compose up -d
