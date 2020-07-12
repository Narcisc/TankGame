using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TankGame.API.Entities
{
    /// <summary>
    /// Entity for game definition
    /// </summary>
    public class GameBattle:BaseEntity
    {
        [Key]
        public int GameBattleId { get; set; }
        [Required]

        [ForeignKey("BlueTankModelId")]
        public TankModel BlueTankModel { get; set; }
        public int BlueTankModelId { get; set; }

        [Required]
        public int BlueTankX { get; set; }

        [Required]
        public int BlueTankY { get; set; }

        [ForeignKey("RedTankModelId")]
        public TankModel RedTankModel { get; set; }
        
        [Required]
        public int RedTankX { get; set; }

        [Required]
        public int RedTankY { get; set; }
        [Required]
        public int RedTankModelId { get; set; }

        [ForeignKey("GameMapId")]
        public GameMap GameMap { get; set; }
        public int GameMapId { get; set; }
        
        public ICollection<GameSimulation> Simulations { get; set; } = new List<GameSimulation>();
    }
}

