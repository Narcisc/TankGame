using System;

namespace TankGame.API.Models
{
    /// <summary>
    /// View model for game simulation
    /// </summary>
    public class GameSimulationDto
    {
        public int GameSimulationId { get; set; }
        public int GameBattleId { get; set; }
        public string Simulation { get; set; }
        public DateTime CreatedTime { get; set; }
        public bool IsActive { get; set; }
    }
}
