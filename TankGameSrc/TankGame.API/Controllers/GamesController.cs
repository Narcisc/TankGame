using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TankGame.API.Entities;
using TankGame.API.Models;
using TankGame.API.Services;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;


namespace TankGame.API.Controllers
{
    /// <summary>
    /// Api methods for Games 
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class GamesController : ControllerBase
    {
        private IGameRepository _gameRepository;
        private IGameMapRepository _gameMapRepository;
        private readonly ITankModelRepository _tankModelRepository;
        private readonly IMapper _mapper;

        public GamesController(IGameRepository gameRepository, IGameMapRepository gameMapRepository, ITankModelRepository tankModelRepository,IMapper mapper)
        {
            _gameRepository = gameRepository;
            _gameMapRepository = gameMapRepository;
            _tankModelRepository = tankModelRepository;
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper)); ;
        }

        /// <summary>
        /// Get game from database
        /// </summary>
        /// <param name="gameId"></param>
        /// <returns>Game from database</returns>
        [Route("{gameid}")]
        [HttpGet("{gameId}", Name = "GetGame")]
        public async Task<ActionResult> GetGame(int gameId)
        {
            var game = await _gameRepository.GetById(gameId);

            if (game == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<GameBattleDto>(game));
        }

        /// <summary>
        /// Get map's game
        /// </summary>
        /// <param name="gameId">Game id</param>
        /// <returns>Map of game</returns>
        [Route("getmapgame/{gameid}")]
        [HttpGet("{gameId}", Name = "GetMapGame")]
        public async Task<ActionResult> GetMapGame(int gameId)
        {
            var game = await _gameRepository.GetById(gameId);

            if (game == null)
            {
                return NotFound();
            }
            var gameDb = _gameRepository.GetFullGame(gameId);
            if (gameDb.GameMap == null)
            {
                return NotFound();
            }
            return Ok(gameDb.GameMap.Map);
        }

        /// <summary>
        /// Get tank combatants
        /// </summary>
        /// <param name="gameId">Game id</param>
        /// <param name="blueTank">True get blue tank, false get red tank</param>
        /// <returns>Tank combatant</returns>
        [Route("getcombattank/{gameid}/{blueTank}")]
        [HttpGet("{gameId}", Name = "GetCombatTank")]
        public async Task<ActionResult> GetCombatTankGame(int gameId, bool blueTank = true)
        {
            var game = await _gameRepository.GetById(gameId);

            if (game == null)
            {
                return NotFound();
            }
            var gameDb = _gameRepository.GetFullGame(gameId);

            if (blueTank)
            {
                if (gameDb.BlueTankModel == null)
                {
                    return NotFound();
                }
                return Ok(_mapper.Map<TankModelDto>(gameDb.BlueTankModel));
            }
            else
            {
                if (gameDb.RedTankModel == null)
                {
                    return NotFound();
                }
                return Ok(_mapper.Map<TankModelDto>(gameDb.RedTankModel));
            }
        }

        /// <summary>
        /// Get start posintion in specifed game for specified tank
        /// </summary>
        /// <param name="gameId">Game id</param>
        /// <param name="blueTank">True get blue tank, false get red tank</param>
        /// <returns>A 2 length array, with x and y coordonates </returns>
        [Route("getstartpositionscombattank/{gameid}/{blueTank}")]
        [HttpGet("{gameId}", Name = "GetStartPositionsCombatTank")]
        public async Task<ActionResult> GetInitalPositionsCombatTankGame(int gameId, bool blueTank = true)
        {
            var game = await _gameRepository.GetById(gameId);

            if (blueTank) return Ok(new int[2] { game.BlueTankX, game.BlueTankY } );
            return Ok(new int[2] { game.RedTankX, game.RedTankY } );
        }

        /// <summary>
        /// Get all games
        /// </summary>
        /// <returns>List of all games</returns>
        [HttpGet(Name = "GetGames")]
        public async Task<ActionResult<IEnumerable<GameBattleDto>>> GetGames()
        {
            var games = await _gameRepository.GetAllAsync();
            if (games == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<IEnumerable<GameBattleDto>>(games));
        }

        /// <summary>
        /// Create a game and save into database
        /// </summary>
        /// <param name="game">GameBattleDto object</param>
        /// <returns>Game created</returns>
        [Route("creategame")]
        [HttpPost]
        public async Task<ActionResult> CreateGame(GameBattleDto game)
        {
            var gameEntity = _mapper.Map<GameBattle>(game);

            var blueTank = await _tankModelRepository.GetById(gameEntity.BlueTankModelId);

            if (blueTank == null)
            {
                return NotFound("Blue Tank not in database");
            }

            var redTank = await _tankModelRepository.GetById(gameEntity.RedTankModelId);

            if (redTank == null)
            {
                return NotFound("Red Tank not in database");
            }

            var gameMap = await _gameMapRepository.GetById(gameEntity.GameMapId);

            if (gameMap == null)
            {
                return NotFound("Map is not in database");
            }

            gameEntity.IsActive = true;
            gameEntity.CreatedTime = DateTime.Now;

            var newGame = await _gameRepository.Add(gameEntity);

            if (newGame == null)
            {
                return BadRequest("Unable to insert game battle");
            }
            //_unitOfWork.SaveChanges();
            return CreatedAtRoute("GetGame", new { gameId = newGame.GameBattleId }, newGame);
        }

        /// <summary>
        /// Update game into database
        /// </summary>
        /// <param name="gameId">Game id</param>
        /// <param name="game">Game details</param>
        /// <returns>Updated game</returns>
        [Route("updategames")]
        [HttpPut("{gameId}")]
        public async Task<ActionResult> UpdateGame(int gameId, [FromBody] GameBattleDto game)
        {
            var dbGame = _mapper.Map<GameBattle>(game);
            var updated = await _gameRepository.UpdateAsync(dbGame, gameId);
            if (updated == null)
            {
                return NotFound();
            }

            return CreatedAtRoute("GetMap", new { gameMapId = updated.GameMapId }, updated);
        }


        /// <summary>
        /// Remove game from database
        /// </summary>
        /// <param name="gameId">Game id</param>
        /// <returns>No content</returns>
        [Route("deletegame")]
        [HttpDelete("{gameId}")]
        public async Task<ActionResult> DeleteGame(int gameId)
        {
            var resultDelete = await _gameRepository.DeleteAsync(gameId);

            if (resultDelete == false)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
