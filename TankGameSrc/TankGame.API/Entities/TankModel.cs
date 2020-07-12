using System.ComponentModel.DataAnnotations;

namespace TankGame.API.Entities
{
    /// <summary>
    /// Enntity for tank models
    /// </summary>
    public class TankModel : BaseEntity
    {
        [Key]
        public int TankModelId { get; set; }

        [Required]
        [MaxLength(300)]
        public string TankModelName { get; set; }

        [Required]
        [MaxLength(1000)]
        public string TankModelDescription { get; set; }

        [Required]
        public int Speed { get; set; }

        [Required]
        public int GunRange { get; set; }

        [Required]
        public double GunPower { get; set; }

        [Required]
        public double ShieldLife { get; set; }
    }
}
