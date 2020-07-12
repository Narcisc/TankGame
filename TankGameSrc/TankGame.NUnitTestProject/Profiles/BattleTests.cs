using NUnit.Framework;
using TankGame.API.Profiles;
using System;
using System.Collections.Generic;
using System.Text;
using TankGame.API.Entities;
using TankGame.API.Models;
using AutoMapper;
using TankGame.API.Helpers;

namespace TankGame.API.Profiles.Tests
{
    [TestFixture()]
    public class BattleTests
    {
        [Test()]
        public void CreateMap()
        {
            var map = new int[][]
                {

                    new int[]{0,1,0,0 },
                    new int[]{0,1,0,0 },
                    new int[]{0,0,0,0 }
                };


            var gameEntiy = new GameMap
            {

                Height = 3,
                Width = 4,
                NoObstacles = 2,
                Map = map.ToJson()
            };

            

            Assert.AreEqual(1,1);
        }
    }
}