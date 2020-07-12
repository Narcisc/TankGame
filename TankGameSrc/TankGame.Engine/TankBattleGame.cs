using TankGame.Tools.Algorithms;
using TankGame.Tools.BasicObjects;
using System;
using System.Collections.Generic;
using System.Linq;

namespace TankGame.Engine
{
    /// <summary>
    /// Class represents game of 2 tank's turn based battle
    /// </summary>
    public class TankBattleGame
    {
        internal readonly int[][] GameMap;
        internal CombatTank BlueTank;
        internal CombatTank RedTank;
        private readonly long MaxTurns;
       
        private readonly AStar PathAlgo;

        /// <summary>
        /// Init batle game between two tanks set on a 2d map
        /// </summary>
        /// <param name="map">Map is stored as int map; value 0 means empty, other value (1) means not empty</param>
        /// <param name="blueTankModel">First tank in battlefield</param>
        /// <param name="posBlue">2 length array represents postion of blue tank on map</param>
        /// <param name="redTankModel">second tank in battlefield</param>
        /// <param name="posRed">2 length array represents postion of red tank on map</param>
        /// <param name="maxTurns">Maximum number of game's turns</param>
        public TankBattleGame(int[][] map, TankModel blueTankModel, int[] posBlue, TankModel redTankModel, int[] posRed, int maxTurns = 100)
        {
            MaxTurns = maxTurns;
            GameMap = map;
            var validator = new TankModelValidator();

            var resultBlueTankModel = validator.Validate(blueTankModel);
            if (!resultBlueTankModel.IsValid) throw new Exception($"Tank model failed, Exception message{resultBlueTankModel.Errors[0].ErrorMessage}");

            var resultRedTankModel = validator.Validate(redTankModel);
            if (!resultRedTankModel.IsValid) throw new Exception($"Tank model failed, Exception message{resultRedTankModel.Errors[0].ErrorMessage}");

            BlueTank = new CombatTank(posBlue[0], posBlue[1], blueTankModel);
            RedTank = new CombatTank(posRed[0], posRed[1], redTankModel);
            PathAlgo = new AStar();
        }

        /// <summary>
        /// Simulate battle between 2 tanks. Blue tank starts the game
        /// </summary>
        /// <returns></returns>
        public List<GameState> SimulateBattle()
        {
            var lst = new List<GameState>();

            var gameState = new GameState(BlueTank, RedTank);
            lst.Add(gameState);
            var roundNo = 1;
            while( BlueTank.ShieldLife >0 && RedTank.ShieldLife >0 && roundNo < MaxTurns)
            {
                lst.Add(BattleRound(roundNo));
                roundNo++;
            }
            return lst;
        }

        /// <summary>
        /// Alternate between blue and red turn
        /// </summary>
        /// <param name="roundNo"></param>
        /// <returns></returns>
        private GameState BattleRound(int roundNo)
        {
            if (roundNo % 2 == 1) return TankAction(BlueTank, RedTank, true);
            else return TankAction(RedTank, BlueTank, false);
        }

        private GameState TankAction(CombatTank  t1, CombatTank t2, bool blueRound)
        {
            var xEnemy = t2.PosX;
            var yEnemy = t2.PosY;
            var step = 0;
            var gamePath = PathAlgo.GetPath(GameMap, new Point2D(t1.PosX, t1.PosY),new Point2D(t2.PosX, t2.PosY),
                                         GameDiagonalMovement.Never).ToArray();
            do
            {
                if (t1.IsEnemyInRange(xEnemy, yEnemy))
                {
                    if (t1.CanShoutEnemy(xEnemy, yEnemy, GameMap))
                    {
                        t2.ShieldLife -= t1.GunPower;
                        break;
                    }
                }
                step++;
                if(step < gamePath.Length)
                {
                    t1.PosX = gamePath[step].X;
                    t1.PosY = gamePath[step].Y;
                }
            } while (step < t1.Speed);
            return blueRound ? new GameState(t1, t2) : new GameState(t2, t1);
        }
    }
}
