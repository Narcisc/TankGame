using TankGame.Tools.BasicObjects;
using NUnit.Framework;

namespace TankGame.Tools.Algorithms.Tests
{
    [TestFixture()]
    public class AstarUnitTest
    {
        int[][] map;

        [SetUp]
        public void RunBeforeAnyTests()
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
        }


        [TestCase(new int[]{0,0},new int[]{0,1})]
        public void GetSimplePathTest(int[] startP, int[] endP)
        {
            //arrange
            var start = new Point2D( startP[0], startP[1] );
            var end = new Point2D(endP[0], endP[1]);
            var game = new AStar();
            //action
            var path = game.GetPath(map, start, end, GameDiagonalMovement.Never);
            //assert
            Assert.AreEqual(path.Count, 2);
        }


        [TestCase(new int[] { 0, 0 }, new int[] { 5, 5 }, 11)]
        [TestCase(new int[] { 0, 0 }, new int[] { 6, 3 }, 12)]
        public void GetPathWitObstacleTest(int[] startP, int[] endP, int expectedResult)
        {
            //arrange
            var game = new AStar();
            //action
            var path = game.GetPath(map, new Point2D(startP[0], startP[1]), new Point2D(endP[0], endP[1]), GameDiagonalMovement.Never);
            //assert
            Assert.AreEqual(path.Count, expectedResult);
        }
    }
}
