# TankGame
Simulate battle between 2 tanks on a 2d map

To run project:
Prerequisities: 
  Docker(Linux containers) and Docker Composer
  
Enter in folder TankGameSrc and run:
  docker-compose build
Then
  docker-compose up  -d
  
Application run here: http://localhost:55908/
Use Postman to check API. 

One scenario can be:
1.	Create blue tank	POST	http://localhost:55908/api/tankmodels	
Object to send: 
{
 ""tankModelName"": ""Panzer IV"",
 ""tankModelDescription"": ""Legendary german tank Panzer IV"",
 ""speed"": 3,
 ""gunRange"": 9,
 ""gunPower"": 300.0,
 ""shieldLife"": 1300.0
 }
2.  Create red tank	POST	http://localhost:55908/api/tankmodels	
Object to send: 
{
    ""tankModelName"": ""T-34"",
    ""tankModelDescription"": ""Legendary soviet tank T-34"",
    ""speed"": 2,
    ""gunRange"": 7,
    ""gunPower"": 290.0,
    ""shieldLife"": 1500.0
}
3	Generate map	POST	http://localhost:55908/api/GameMaps/GenerateGameMap/10/10/0		
3.1.	Set obstacles	PUT	http://localhost:55908/api/GameMaps/gameid/xPos/yPos/true		
4.	 Create game. Choose red and blue tank, map and start postions for both tanks	POST	http://localhost:55908/api/games/CreateGame	
Object to send 
{
        ""blueTankModelId"":1,
        ""blueTankX"": 0,
        ""blueTankY"": 0,
        ""redTankModelId"": 2,
        ""redTankX"": 2,
        ""redTankY"": 9,
        ""gameMapId"": 1
   }
5.	Generate battle simulation for game created	POST	http://localhost:55908/api/battle/1/generateSimulation	
6.	Get Winner	GET	http://localhost:55908/api/battle/1/simulations/1/winner

Projects in solution:

TankGame.Tools - c# library, use a Astar algorithm (EpPathFindings) to find path between 2 points in plan 
TankGame.Engine - define games objects and simulate battle
TankGame.NUnitTestProject - few test units using nUnit
TankGame.API - webapi over .net core to save data into Postgresql database

Used: 
  C#, .net core, EntityFramework,
  GenericRepository, 
  AutoMapper,
  FluentValidation,
  Postgreql,
  NUnit
 
 
