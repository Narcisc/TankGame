using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using TankGame.API.Entities;
using TankGame.API.Helpers;
using TankGame.API.Models;
using TankGame.API.Services;
using TankGame.Engine;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TankGame.API.Controllers
{
    /// <summary>
    /// API create battle simulation
    /// </summary>
    [ApiController]
    public class SimulationsController : ControllerBase
    {
        private IGameRepository _gameRepository;
        private ISimulationRepository _simulationRepository;
        private readonly IMapper _mapper;

        public SimulationsController(IGameRepository gameRepository, ISimulationRepository simulationRepository,  IMapper mapper)
        {
            _gameRepository = gameRepository;
            _simulationRepository = simulationRepository;
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper)); ;
        }

        /// <summary>
        /// Get all simulations from database for a specified game id
        /// </summary>
        /// <param name="gameId">Game id</param>
        /// <returns>All battle simulations </returns>
        [Route("api/battle/{gameid}/simulations")]
        [HttpGet("{gameId}", Name = "GetSimulationsForGame")]
        public ActionResult<IEnumerable<GameSimulationDto>> GetSimulationsForGame(int gameId)
        {
            if (_gameRepository.GetById(gameId) == null)
            {
                return NotFound();
            }

            var simulations =  _simulationRepository.GetWhere(x => x.GameBattleId == gameId).Result;

            if (simulations == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<IEnumerable<GameSimulationDto>>(simulations));
        }

        /// <summary>
        /// Get requested simulation for specified game id
        /// </summary>
        /// <param name="gameId">Game id</param>
        /// <param name="simulationId">Simulation id</param>
        /// <returns>A simulation of battle</returns>
        [Route("api/battle/{gameid}/simulations/{simulationId}")]
        [HttpGet("{gameId}/{simulationId}", Name = "GetSimulationForGame")]
        public async Task<ActionResult<IEnumerable<GameSimulationDto>>> GetSimulationForGame(int gameId, int simulationId)
        {
            if (_gameRepository.GetById(gameId) == null)
            {
                return NotFound();
            }

            var simulation = await _simulationRepository.FindAsync(x => x.GameBattleId == gameId && x.GameSimulationId == simulationId);
            if (simulation == null)
            {
                return NotFound();
            }
            var dbGame = _gameRepository.GetFullGame(gameId);
            simulation.GameBattle = dbGame;
            return Ok(_mapper.Map<GameSimulationDto>(simulation));
        }


        /// <summary>
        /// Get information about winner of a simulation
        /// </summary>
        /// <param name="gameId">Game id</param>
        /// <param name="simulationId">Simulation id</param>
        /// <returns>String with info about winner</returns>
        [Route("api/battle/{gameid}/simulations/{simulationId}/winner")]
        [HttpGet("{gameId}/{simulationId}", Name = "GetSimulationWinner")]
        public async Task<ActionResult<IEnumerable<GameSimulationDto>>> GetSimulationWinner(int gameId, int simulationId)
        {
            var gameDb = _gameRepository.GetFullGame(gameId);

            if (gameDb == null)
            {
                return NotFound();
            }

            var simulation = await _simulationRepository.FindAsync(x => x.GameBattleId == gameId && x.GameSimulationId == simulationId);
            if (simulation == null)
            {
                return NotFound();
            }
            var noRounds = simulation.Simulation.ToList().Count;
            var winner = (noRounds % 2 == 0) ? gameDb.BlueTankModel.TankModelName : gameDb.RedTankModel.TankModelName;
            var strResult = $"Battle between blue tank {gameDb.BlueTankModel.TankModelName} and red tank {gameDb.RedTankModel.TankModelName} was won by {winner} in {noRounds} rounds";
            
            return Ok(strResult);
        }


        /// <summary>
        /// Generate simulation and save ito database
        /// </summary>
        /// <param name="gameId">Game id</param>
        /// <returns>String with info about winner</returns>
        [Route("api/battle/{gameid}/generateSimulation")]
        [HttpPost]
        public async Task<ActionResult<GameSimulationDto>> GenerateSimulation(int gameId)
        {
            var gameDb = _gameRepository.GetFullGame(gameId);

            if (gameDb == null)
            {
                return NotFound();
            }
            var blueTank = new Engine.TankModel
            {
                ModelName = gameDb.BlueTankModel.TankModelName,
                GunPower = gameDb.BlueTankModel.GunPower,
                ShieldLife = gameDb.BlueTankModel.ShieldLife,
                GunRange = gameDb.BlueTankModel.GunRange,
                Speed = gameDb.BlueTankModel.Speed
            };
            var redTank = new Engine.TankModel
            {
                ModelName = gameDb.RedTankModel.TankModelName,
                GunPower = gameDb.RedTankModel.GunPower,
                ShieldLife = gameDb.RedTankModel.ShieldLife,
                GunRange = gameDb.RedTankModel.GunRange,
                Speed = gameDb.RedTankModel.Speed
            };
            var map = gameDb.GameMap.Map.ToMatrix();
            var game = new TankBattleGame(map,
                blueTank, new int[2] { gameDb.BlueTankX, gameDb.BlueTankY },
                redTank, new int[2] { gameDb.RedTankX, gameDb.RedTankY });

            var simulation = game.SimulateBattle();

            var gameSimulation = new GameSimulation
            {
                GameBattleId = gameId,
                Simulation = JsonSerializer.Serialize(simulation),
                IsActive = true,
                CreatedTime = DateTime.Now
            };
            var newSimulation = await _simulationRepository.Add(gameSimulation);
            if (newSimulation == null)
            {
                return BadRequest("Unable to generate simulation");
            }
            var dbGame = _gameRepository.GetFullGame(gameId);
            newSimulation.GameBattle = dbGame;
            return CreatedAtRoute("GetSimulationForGame", new { gameId = gameId, simulationId = newSimulation.GameSimulationId }
            , newSimulation);
        }

        /// <summary>
        /// Remove simulation from database
        /// </summary>
        /// <param name="simulationId">Simulation id</param>
        /// <returns>No content</returns>
        [HttpDelete("{simulationId}")]
        public async Task<ActionResult> Delete(int simulationId)
        {
            var resultDelete = await _simulationRepository.DeleteAsync(simulationId);

            if (resultDelete == false)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
