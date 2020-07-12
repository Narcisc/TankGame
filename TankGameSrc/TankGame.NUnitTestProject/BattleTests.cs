using NUnit.Framework;
using TankGame.Engine;
using System;
using System.Collections.Generic;
using System.Text;

namespace TankGame.Engine.Tests
{
    [TestFixture()]
    public class BattleTests
    {
        int[][] map;
        TankModel panzer;
        TankModel T60;

        [SetUp]
        public void RunBeforeAnyTests()
        {
            

            panzer = new TankModel
            {
                ModelName = "Panzer",
                Speed = 2,
                ShieldLife = 1500,
                GunPower = 300,
                GunRange = 5
            };
            T60 = new TankModel
            {
                ModelName = "T60",
                Speed = 3,
                ShieldLife = 1200,
                GunPower = 200,
                GunRange = 6
            };
        }

        [Test()]
        public void SimulateBattle()
        {
            map = new int[][]
                       {
                            new int[] {0, 0, 0, 0, 0, 0, 0, 0},
                            new int[] {0, 0, 0, 0, 0, 0, 0, 0},
                            new int[] {0, 0, 0, 1, 0, 0, 0, 0},
                            new int[] {0, 0, 0, 1, 0, 0, 0, 0},
                            new int[] {0, 0, 0, 1, 0, 0, 0, 0},
                            new int[] {1, 0, 1, 0, 0, 0, 0, 0},
                            new int[] {1, 0, 1, 0, 0, 0, 0, 0},
                            new int[] {1, 1, 1, 1, 1, 1, 0, 0},
                            new int[] {1, 0, 1, 0, 0, 0, 0, 0},
                            new int[] {1, 0, 1, 1, 0, 0, 0, 0}
                       };
            //arrange
            var game = new TankBattleGame(map, panzer, new int[2] { 0, 0 }, T60, new int[2] { 1, 7 });

            //action
            var list = game.SimulateBattle();
            //assert
            Assert.AreEqual(12, list.Count);
        }

        [Test()]
        public void SimulateBattle_NoPath()
        {
            map = new int[][]
                       {
                            new int[] {0, 0, 0, 0, 0, 0, 0, 0},
                            new int[] {1, 1, 1, 1, 1, 1, 1, 1},
                            new int[] {0, 0, 0, 1, 0, 0, 0, 0},
                            new int[] {0, 0, 0, 1, 0, 0, 0, 0},
                            new int[] {0, 0, 0, 1, 0, 0, 0, 0},
                            new int[] {1, 0, 1, 0, 0, 0, 0, 0},
                            new int[] {1, 0, 1, 0, 0, 0, 0, 0},
                            new int[] {1, 1, 1, 1, 1, 1, 0, 0},
                            new int[] {1, 0, 1, 0, 0, 0, 0, 0},
                            new int[] {1, 0, 1, 1, 0, 0, 0, 0}
                       };
            //arrange
            var game = new TankBattleGame(map, panzer, new int[2] { 0, 0 }, T60, new int[2] { 2, 7 });

            //action
            var list = game.SimulateBattle();
            //assert
            Assert.AreEqual(100, list.Count);
        }

        [Test()]
        public void SimulateBattle_BigMap()
        {
            map = new int[][]
                       {
                            new int[] {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                            new int[] {1, 0, 1, 0, 1, 1, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                            new int[] {0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                            new int[] {0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                            new int[] {0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                            new int[] {1, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                            new int[] {1, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                            new int[] {1, 1, 1, 1, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                            new int[] {1, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                            new int[] {1, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                            new int[] {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                            new int[] {1, 0, 1, 0, 1, 1, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                            new int[] {0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                            new int[] {0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                            new int[] {0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0},
                            new int[] {1, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0},
                            new int[] {1, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0},
                            new int[] {1, 1, 1, 1, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                            new int[] {1, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                            new int[] {1, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                       };
            //arrange
            var game = new TankBattleGame(map, panzer, new int[2] { 0, 0 }, T60, new int[2] { 19, 30 });

            //action
            var list = game.SimulateBattle();
            //assert
            Assert.AreEqual(26, list.Count);
        }
    }
}