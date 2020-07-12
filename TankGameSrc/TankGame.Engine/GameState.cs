using System;

namespace TankGame.Engine
{
    /// <summary>
    /// This class keep the state of the tanks at the end of a turn
    /// </summary>
    [Serializable]
    public class GameState
    {
        public  CombatTank BlueTank { get; set; }
        public CombatTank RedTank { get; set; }

        public GameState()
        {

        }
        public GameState(CombatTank blueTank, CombatTank redTank)
        {
            BlueTank = new CombatTank(blueTank.ModelName, blueTank.Speed, blueTank.GunRange, blueTank.GunPower, blueTank.ShieldLife, blueTank.PosX, blueTank.PosY);
            RedTank = new CombatTank(redTank.ModelName, redTank.Speed, redTank.GunRange, redTank.GunPower, redTank.ShieldLife, redTank.PosX, redTank.PosY);
        }
    }
}
