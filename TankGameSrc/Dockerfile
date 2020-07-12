#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

#Depending on the operating system of the host machines(s) that will build or run the containers, the image specified in the FROM statement may need to be changed.
#For more information, please see https://aka.ms/containercompat



FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
ENV ASPNETCORE_URLS=http://+:5000
WORKDIR /app


COPY ./*.sln .
COPY TankGame.API/*.csproj ./TankGame.API/
COPY TankGame.Engine/*.csproj ./TankGame.Engine/
COPY TankGame.Tools/*.csproj ./TankGame.Tools/
COPY TankGame.NUnitTestProject/*.csproj ./TankGame.NUnitTestProject/ 
#
RUN dotnet restore 
#
# copy everything else and build app
COPY TankGame.API/. ./TankGame.API/
COPY TankGame.Engine/. ./TankGame.Engine/
COPY TankGame.Tools/. ./TankGame.Tools/ 
#
WORKDIR /app/TankGame.API
RUN dotnet publish -c Debug -o out 
#
FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS runtime
ENV ASPNETCORE_URLS=http://+:5000
WORKDIR /app 
#
COPY --from=build /app/TankGame.API/out ./
ENTRYPOINT ["dotnet", "TankGame.API.dll"]