using System;

namespace TankGame.Engine
{
    /// <summary>
    /// A tank in battlefield. Tank class extends tank model class adding plan coordontes (X,Y) in map
    /// </summary>
    [Serializable]
    public class CombatTank : TankModel
    {
        public CombatTank()
        {

        }
        public CombatTank(string name, int speed, int range, double power, double life, int x, int y) : base(name, speed, range, power, life)
        {
            PosX = x;
            PosY = y;
        }

        public CombatTank(int x, int y, TankModel m) : base(m.ModelName, m.Speed, m.GunRange, m.GunPower, m.ShieldLife)
        {
            PosX = x;
            PosY = y;
        }
        public int PosX { get; set; }
        public int PosY { get; set; }

        
        /// <summary>
        /// Check if enemy tank is in gun's range
        /// </summary>
        /// <param name="enemyX">Enemy X position</param>
        /// <param name="enemyY">Enemy Y position</param>
        /// <returns></returns>
        public bool IsEnemyInRange( int enemyX, int enemyY)
        {
            return GunRange >= Math.Sqrt((PosX - enemyX) * (PosX - enemyX) + (PosY - enemyY) * (PosY - enemyY));
        }

        /// <summary>
        /// A tank can fire only streight line. This methof check if tanks are both on the same row or coloumn and only empty spaces between the two tanks
        /// </summary>
        /// <param name="enemyX">Enemy X position</param>
        /// <param name="enemyY">Enemy Y position</param>
        /// <param name="map">Map where the two tanks fight each other</param>
        /// <returns></returns>
        public bool CanShoutEnemy(int enemyX, int enemyY, int[][] map)
        {
            if (PosY == enemyY)
            {
                var x1 = Math.Min(PosX, enemyX) + 1;
                var x2 = Math.Max(PosX, enemyX) - 1;
                for(var i = x1; i <=x2; ++i)
                {
                    if (map[i][PosY] != 0) return false;
                }
                return true;
            }
            else if (PosX == enemyX)
            {
                var y1 = Math.Min(PosY, enemyY) + 1;
                var y2 = Math.Max(PosY, enemyY) - 1;
                for (var j = y1; j <= y2; ++j)
                {
                    if (map[PosX][j] != 0) return false;
                }
                return true;
            }
            else return false;


        }
    }
}
