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
    /// Api methods for tank models 
    /// </summary>
    [Route("api/tankmodels")]
    [ApiController]
    public class TankModelsController : ControllerBase
    {
        private readonly ITankModelRepository _tankModelRepository;
        private readonly IMapper _mapper;


        public TankModelsController(ITankModelRepository tankModelRepository, IMapper mapper )
        {
            _tankModelRepository = tankModelRepository;
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        /// <summary>
        /// Get tank model from database
        /// </summary>
        /// <param name="tankModelId">Id tank model</param>
        /// <returns>Tank model from database</returns>
        [HttpGet("{tankModelId}", Name = "GetTankModel")]
        public async Task<ActionResult> GetTankModel(int tankModelId)
        {
            var tankModel = await _tankModelRepository.GetById(tankModelId);

            if (tankModel == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<TankModelDto>(tankModel));
        }


        /// <summary>
        /// Get all tank models from database
        /// </summary>
        /// <returns>Array of tank models</returns>
        [HttpGet(Name = "GetTankModels")]
        public async Task<ActionResult<IEnumerable<TankModelDto>>> GetTankModels()
        {
            var tankModels = await _tankModelRepository.GetAllAsync();
            if (tankModels == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map< IEnumerable<TankModelDto>>(tankModels));
        }

        /// <summary>
        /// Create a tank model and save into database
        /// </summary>
        /// <param name="tankModel">Tank model details</param>
        /// <returns>Created tank model</returns>
        [HttpPost]
        public async Task<ActionResult> CreateTankModel([FromBody] TankModelDto tankModel)
        {
            var tankModelEntity = _mapper.Map<TankModel>(tankModel);

            tankModelEntity.IsActive = true;
            tankModelEntity.CreatedTime = DateTime.Now;
            var newTankModel = await _tankModelRepository.Add(tankModelEntity);
            
            if (newTankModel == null)
            {
                return BadRequest("Unable to insert tank model");
            }
            return CreatedAtRoute("GetTankModel", new { tankModelId = newTankModel.TankModelId }, newTankModel);

        }


        /// <summary>
        /// Update tank model into database
        /// </summary>
        /// <param name="tankModelId">Tank model id</param>
        /// <param name="tankModel">Tank model details</param>
        /// <returns>Updated tank model</returns>
        [HttpPut("{tankModelId}")]
        public async Task<ActionResult> UpdateTankModel(int tankModelId, [FromBody] TankModelDto tankModel)
        {
            var dbTankModel = _mapper.Map<TankModel>(tankModel);

            var updated = await _tankModelRepository.UpdateAsync(dbTankModel, tankModelId);
            if (updated == null)
            {
                return NotFound();
            }
            return CreatedAtRoute("GetTankModel", new { tankModelId = updated.TankModelId }, updated);
        }

        /// <summary>
        /// Remove tank model from database
        /// </summary>
        /// <param name="tankModelId">Tank model id</param>
        /// <returns>No content</returns>
        [HttpDelete("{tankModelId}")]
        public async Task<ActionResult> Delete(int tankModelId)
        {
            var resultDelete = await _tankModelRepository.DeleteAsync(tankModelId);

            if (resultDelete == false)
            {
                return NotFound();
            }
            return NoContent();
        }
       
    }
}
