using System;

namespace TankGame.API.Models
{
    /// <summary>
    /// View model for tank
    /// </summary>
    public class TankModelDto
    {
        public int TankModelId { get; set; }
        public string TankModelName { get; set; }
        public string TankModelDescription { get; set; }
        public int Speed { get; set; }
        public int GunRange { get; set; }
        public double GunPower { get; set; }
        public double ShieldLife { get; set; }
        public DateTime CreatedTime { get; set; }
        public bool IsActive { get; set; }
    }
}
