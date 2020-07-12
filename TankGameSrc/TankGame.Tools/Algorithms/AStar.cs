using TankGame.Tools.BasicObjects;
using EpPathFinding.cs;
using System.Collections.Generic;
using System.Linq;

namespace TankGame.Tools.Algorithms
{
    public class AStar : IAstar
    {
        BaseGrid Board;
        GridPos StartPos;
        GridPos EndPos;
        
        public IList<Point2D> GetPath(int[][] grid, Point2D start, Point2D end, GameDiagonalMovement type)
        {
            var width = grid.Length;
            var height = grid[0].Length;
            bool[][] movableMatrix = new bool[width][];
            for (int widthTrav = 0; widthTrav < width; widthTrav++)
            {
                movableMatrix[widthTrav] = new bool[height];
                for (int heightTrav = 0; heightTrav < height; heightTrav++)
                {
                    movableMatrix[widthTrav][heightTrav] = grid[widthTrav][heightTrav] == 0;
                }
            }

            Board = new StaticGrid(width, height, movableMatrix);
            StartPos = new GridPos(start.X, start.Y);
            EndPos = new GridPos(end.X, end.Y);
            JumpPointParam jpParam = new JumpPointParam(Board, StartPos, EndPos, EndNodeUnWalkableTreatment.DISALLOW, 
                                                type == GameDiagonalMovement.Never ? DiagonalMovement.Never : DiagonalMovement.Always, 
                                                HeuristicMode.EUCLIDEAN);
            List<GridPos> resultPathList = JumpPointFinder.FindPath(jpParam);


            return resultPathList.Select(x => new Point2D(x.x, x.y)).ToList();
        }


       

        
    }
}
