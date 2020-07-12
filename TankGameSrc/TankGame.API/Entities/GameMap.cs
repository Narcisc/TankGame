using System.ComponentModel.DataAnnotations;

namespace TankGame.API.Entities
{
    /// <summary>
    /// Entity for game maps
    /// </summary>
    public class GameMap : BaseEntity
    {
        [Key]
        public int GameMapId { get; set; }

        [Required]
        public string Map { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public int NoObstacles { get; set; }

    }
}
