using TankGame.Tools.BasicObjects;
using EpPathFinding.cs;
using System.Collections.Generic;


namespace TankGame.Tools.Algorithms
{
    public interface IAstar
    {
        IList<Point2D> GetPath(int[][] grid, Point2D start, Point2D end, GameDiagonalMovement type);
    }
}
