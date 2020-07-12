using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TankGame.API.Entities;
using TankGame.API.Models;
using TankGame.API.Services;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;


namespace TankGame.API.Controllers
{
    /// <summary>
    /// Api methods for GameMaps - generate 2D map, save, update, delete map from database
    /// </summary>
    [ApiController]
    [Route("api/gamemaps")]
    public class GameMapsController : ControllerBase
    {
        private IGameMapRepository _gameMapRepository;
        private readonly IMapper _mapper;

        public GameMapsController(IGameMapRepository gameMapRepository, IMapper mapper)
        {
            _gameMapRepository = gameMapRepository;
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        /// <summary>
        /// Return map for game
        /// </summary>
        /// <param name="gameMapId">Id for the map</param>
        /// <returns>Object contains a map which can be used in game</returns>
        [Route("{gamemapid}")]
        [HttpGet("{gamemapid}", Name = "GetMap")]
        public async Task<ActionResult> GetMap(int gameMapId)
        {
            var gameMap = await _gameMapRepository.GetById(gameMapId);

            if (gameMap == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<GameMapDto>(gameMap));
        }

        /// <summary>
        /// Returns all maps from database
        /// </summary>
        /// <returns></returns>
        
        [HttpGet]
        [HttpHead]
        public async Task<ActionResult<IEnumerable<GameMapDto>>> GetMaps()
        {
            var maps = await _gameMapRepository.GetAllAsync();
            if (maps == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<IEnumerable<GameMapDto>>(maps));
        }

   
        [Route("CreateGameMap")]
        [HttpPost(Name = "CreateGameMap")]
        public async Task<ActionResult> CreateGameMap(GameMapDto gameMap)
        {
            var gameMapEntity = _mapper.Map<GameMap>(gameMap);
            var lines = gameMap.Map.Length;
            var columns = gameMap.Map[0].Length;

            gameMapEntity.IsActive = true;
            gameMapEntity.CreatedTime = DateTime.Now;
            gameMapEntity.Height = lines;
            gameMapEntity.Width = columns;
            var noObstacles = 0;
            for (var i = 0; i < lines;i++)
                for (var j = 0; j < columns; ++j)
                    if (gameMap.Map[i][j] == 1) noObstacles++;

            gameMapEntity.NoObstacles = noObstacles;

            var newGameMap = await _gameMapRepository.Add(gameMapEntity);

            if (newGameMap == null)
            {
                return BadRequest("Unable to insert game map");
            }
            return CreatedAtRoute("GetMap", new { gameMapId = newGameMap.GameMapId }, newGameMap);
        }

        
        [Route("GenerateGameMap/{lines}/{columns}/{obstacles}")]
        [HttpGet("{lines}/{columns}/{obstacles}",  Name = "GenerateGameMap")]
        public async Task<ActionResult> GenerateGameMap(int lines, int columns, int  obstacles)
        {
            if (obstacles > lines * columns)
            {
                return BadRequest("Unable to generate game map, too many obstacles");
            }
            if (lines <= 0 || columns <= 0 )
            {
                return BadRequest("Unable to generate game map, lines and columns must be > 0");
            }

            var gameMap = new GameMapDto
            {
                Map = new int[lines][],
                NoObstacles = obstacles,
                Height = lines,
                Width = columns
            };
            for (var i = 0; i < lines; ++i) gameMap.Map[i] = new int[columns];

            var generatedObstacles = 0;
            while(generatedObstacles < obstacles)
            {
                var random = new Random();
                var x = random.Next(lines);
                var y = random.Next(columns);
                if (gameMap.Map[x][y] == 1) continue;
                gameMap.Map[x][y] = 1;
                ++generatedObstacles;
            }
            var gameMapEntity = _mapper.Map<GameMap>(gameMap);
            gameMapEntity.IsActive = true;
            gameMapEntity.CreatedTime = DateTime.Now;

            var newGameMap = await _gameMapRepository.Add(gameMapEntity);

            if (newGameMap == null)
            {
                return BadRequest("Unable to generate game map");
            }
            return CreatedAtRoute("GetMap", new { gameMapId = newGameMap.GameMapId }, newGameMap);
        }


        /// <summary>
        /// Update map into database
        /// </summary>
        /// <param name="gameMapId">Map id</param>
        /// <param name="gameMap">Map details</param>
        /// <returns>Updated map</returns>
        [Route("{gamemapid}")]
        [HttpPut()]
        public async Task<ActionResult> UpdateGameMap(int gameMapId, [FromBody] GameMapDto gameMap)
        {
            var dbGameMap = _mapper.Map<GameMap>(gameMap);
            var updated = await _gameMapRepository.UpdateAsync(dbGameMap, gameMapId);

            if (updated == null)
            {
                return NotFound();
            }

            return CreatedAtRoute("GetMap", new { gameMapId = updated.GameMapId }, updated);
        }


        /// <summary>
        /// Set/remove obstacle for a map at coordonates specified
        /// </summary>
        /// <param name="gameMapId"></param>
        /// <param name="posx"></param>
        /// <param name="posy"></param>
        /// <param name="operation">True set obstacle, false remove obstacle</param>
        /// <returns>Updated map</returns>
        [Route("{gamemapid}/{posx}/{posy}/{operation}")]
        [HttpPut()]
        public async Task<ActionResult> SetObstacle(int gameMapId, int posx, int posy, bool operation)
        {
            GameMap updated;
            if( operation) 
                updated = await _gameMapRepository.SetObstacleAsync(gameMapId, posx, posy);
            else
                updated = await _gameMapRepository.RemoveObstacleAsync(gameMapId, posx, posy);

            if (updated == null)
            {
                return NotFound();
            }

            return CreatedAtRoute("GetMap", new { gameMapId = updated.GameMapId }, updated);
        }

        

        /// <summary>
        /// Remove a map from database
        /// </summary>
        /// <param name="gameMapId"></param>
        /// <returns>No content</returns>
        [Route("{gamemapid}")]
        [HttpDelete]
        public async Task<ActionResult> Delete(int gameMapId)
        {
            var resultDelete = await _gameMapRepository.DeleteAsync(gameMapId);

            if (resultDelete == false)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
