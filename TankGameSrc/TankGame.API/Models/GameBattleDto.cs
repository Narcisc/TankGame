using System;

namespace TankGame.API.Models
{
    /// <summary>
    /// View model for game battle entity
    /// </summary>
    public class GameBattleDto
    {
        public int GameBattleId { get; set; }
        public int BlueTankModelId { get; set; }
        public int BlueTankX { get; set; }
        public int BlueTankY { get; set; }
        public int RedTankModelId { get; set; }
        public int RedTankX { get; set; }
        public int RedTankY { get; set; }
        public int GameMapId { get; set; }
        public DateTime CreatedTime { get; set; }
        public bool IsActive { get; set; }
    }
}
