using System;

namespace TankGame.API.Models
{
    /// <summary>
    /// View model for game map entity
    /// </summary>
    public class GameMapDto
    {
        public int GameMapId { get; set; }
        public int[][] Map { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public int NoObstacles { get; set; }
        public DateTime CreatedTime { get; set; }
        public bool IsActive { get; set; }
    }
}
