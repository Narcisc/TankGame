using System;
using System.Collections.Generic;
using System.Text;

namespace TankGame.Engine
{
    /// <summary>
    /// Tank model
    /// </summary>
    public class TankModel
    {
        /// <summary>
        /// Unique tank model name
        /// </summary>
        public string ModelName { get; set; }
        /// <summary>
        /// Max steps per turn
        /// </summary>
        public int Speed { get; set; }
        /// <summary>
        /// Max distance from enemy when the gun can be used
        /// </summary>
        public int GunRange { get; set; }
        /// <summary>
        /// Damage suffered by enemy when tank fires
        /// </summary>
        public double GunPower { get; set; }
        /// <summary>
        /// Life of the current tank
        /// </summary>
        public double ShieldLife { get; set; }
        public TankModel(string name, int speed, int range, double power, double life)
        {
            ModelName = name;
            Speed = speed;
            GunRange = range;
            GunPower = power;
            ShieldLife = life;
        }
        public TankModel()
        {
        }
    }
}
