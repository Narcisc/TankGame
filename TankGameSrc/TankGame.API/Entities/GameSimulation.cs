using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TankGame.API.Entities
{
    /// <summary>
    /// Entity for battle simulations
    /// </summary>
    public class GameSimulation : BaseEntity
    {
        [Key]
        public int GameSimulationId { get; set; }

        [ForeignKey("GameBattleId")]
        public GameBattle GameBattle { get; set; }

        [Required]
        public int GameBattleId { get; set; }

        
        [Required]
        public string Simulation { get; set; }
    }
}
